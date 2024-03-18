window.loadMap = (mapId, latitude, longitude) => {
    const mapElement = document.getElementById(mapId);
    const latLng = { lat: latitude, lng: longitude };
    const mapOptions = {
        center: latLng,
        zoom: 10 // Adjust zoom level as needed
    };
    const map = new google.maps.Map(mapElement, mapOptions);
    const marker = new google.maps.Marker({
        position: latLng,
        map: map
    });
};
