window.initPlacePicker = (containerId, blazorObjectReference) => {
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
    placePickerContainer.appendChild(input);

    console.log('Input creado, inicializando autocomplete...');

    // Inicializar el autocompletado de Google Places
    const autocomplete = new google.maps.places.Autocomplete(input, {
        types: ['address'],
        componentRestrictions: { country: 'AR' }
    });

    // Escuchar el evento de selección de lugar
    autocomplete.addListener('place_changed', function() {
        console.log('Lugar seleccionado');
        const place = autocomplete.getPlace();
        console.log('Place:', place);
        if (place && place.formatted_address) {
            console.log('Dirección seleccionada:', place.formatted_address);
            blazorObjectReference.invokeMethodAsync('SetAddressFromPicker', place.formatted_address);
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