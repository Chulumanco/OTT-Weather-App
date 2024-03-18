function initialize(latitude, longitude) {
    var latlng = new google.maps.LatLng(latitude, longitude);
    var options = {
        zoom: 14,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };
    var map = new google.maps.Map(document.getElementById("map"), options);

    // Create a marker and set its position
    var marker = new google.maps.Marker({
        position: latlng,
        map: map,
        title: 'you are here' 
    });
}
 
