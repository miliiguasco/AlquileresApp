window.initPlacePicker = (containerId, blazorObjectReference, initialAddress) => {
    console.log('Inicializando place picker...');
    const placePickerContainer = document.getElementById(containerId);
    if (!placePickerContainer) {
        console.error('No se encontró el contenedor:', containerId);
        return;
    }

    // Limpiar el contenedor antes de agregar el input
    placePickerContainer.innerHTML = '';

    // Crear el input para el autocompletado
    const input = document.createElement('input');
    input.type = 'text';
    input.className = 'form-control';
    input.placeholder = 'Ingrese una dirección';
    input.style.width = '100%';
    input.style.height = '100%';

    // Establecer el valor inicial si se proporciona una dirección
    if (initialAddress) {
        input.value = initialAddress;
    }

    placePickerContainer.appendChild(input);

    console.log('Input creado, inicializando autocomplete...');

    const defaultBounds = new google.maps.LatLngBounds(
    new google.maps.LatLng(-90, -180), // Suroeste (Polo Sur, -180 longitud)
    new google.maps.LatLng(90, 180)    // Noreste (Polo Norte, +180 longitud)
    );

    // Inicializar el autocompletado de Google Places
    const autocomplete = new google.maps.places.Autocomplete(input, {
        types: ['address'],
        bounds: defaultBounds,
        strictBounds: false
    });

    // Escuchar el evento de selección de lugar
    autocomplete.addListener('place_changed', function() {
        console.log('Lugar seleccionado');
        const place = autocomplete.getPlace();
        console.log('Place object from Google Maps:', place);

        if (place && place.formatted_address) {
            let locality = ''; // Variable para almacenar la localidad

            // Iterar sobre los componentes de la dirección para encontrar la localidad
            for (let i = 0; i < place.address_components.length; i++) {
                const component = place.address_components[i];
                // Los tipos comunes para localidad son 'locality' o a veces 'political' y 'sublocality'
                if (component.types.includes('locality') || component.types.includes('sublocality') || component.types.includes('administrative_area_level_3')) {
                    locality = component.long_name;
                    break; // Una vez encontrada, salimos del bucle
                }
            }

            console.log('Dirección seleccionada:', place.formatted_address);
            console.log('Localidad extraída:', locality);

            // Invocar el método Blazor con la dirección formateada y la localidad
            blazorObjectReference.invokeMethodAsync('SetAddressAndLocalityFromPicker', place.formatted_address, locality);
        } else {
            console.warn('No se pudo obtener la dirección formateada o el lugar es nulo.');
            // Si no hay dirección formateada, envía cadenas vacías a Blazor
            blazorObjectReference.invokeMethodAsync('SetAddressAndLocalityFromPicker', '', '');
        }
    });

    console.log('Place picker inicializado correctamente');
};

window.mostrarMapa = (elementId, latitud, longitud) => {
    const mapContainer = document.getElementById(elementId);
    if (mapContainer) {
        const ubicacion = { lat: latitud, lng: longitud };
        const map = new google.maps.Map(mapContainer, {
            zoom: 15,
            center: ubicacion,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });

        new google.maps.Marker({
            position: ubicacion,
            map: map,
            title: 'Ubicación de la propiedad'
        });
    }
};
