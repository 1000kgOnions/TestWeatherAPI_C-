
var userip
var ipdatakey = 'f860345bafcdbd87b02660aefe031519d11ac488618b94272a86e16c';

//lấy địa chỉ IP mạng của người dùng
$.getJSON('https://jsonip.com/?callback=?').done(function (data) {
    var ip_address = window.JSON.parse(JSON.stringify(data, null, 2));

    getcity(ip_address.ip)
});


//lấy thành phố của user
function getcity(ip) {
    userip = ip;
    $.getJSON('https://api.ipdata.co/' + userip + '?api-key=' + ipdatakey, function (data) {
        console.log(JSON.stringify(data, null, 2));
        var ok = window.JSON.parse(JSON.stringify(data, null, 2));
        //infocity(ok.city)
        myMap(ok.latitude, ok.longitude)
    });
}

//function infocity(s) {

//    //var lat1 = parseFloat(lat);
//    //var lon1 = parseFloat(lon);
//    var cityname = s
//    $.ajax({
//        //url: "/Home/WeatherDetail?Lat=" + lat1 + "&Lon=" + lon1,
//        url: "/Home/WeatherDetail?City=" + cityname,
//        type: "POST",
//        success: function (rsltval) {
//            var data = JSON.parse(rsltval);
//            console.log(data);
//            $("#lblCity").html(data.City);
//            $("#lblCountry").text(data.Country);
//            $("#lblLat").text(data.Lat);
//            $("#lblLon").text(data.Lon);
//            $("#lblDescription").text(data.Description);
//            $("#lblHumidity").text(data.Humidity);
//            $("#lblTempFeelsLike").text(data.TempFeelsLike);
//            $("#lblTemp").text(data.Temp);
//            $("#lblTempMax").text(data.TempMax);
//            $("#lblTempMin").text(data.TempMin);
//            $("#imgWeatherIconUrl").attr("src", "http://openweathermap.org/img/w/" + data.WeatherIcon + ".png");
//            $("#lblVi").text(data.Visibility / 1000);
//            $("#lblspeed").text(data.Speed);

//        },
//        error: function () {
//        }
//    });
//}
//window.onload = function () { getip()  }
function myMap(lat, lon) {
    lat = parseFloat(lat);
    lon = parseFloat(lon);
    var myCenter = new google.maps.LatLng(lat, lon);
    var mapProp = { center: myCenter, zoom: 10, scrollwheel: false, draggable: true, mapTypeId: google.maps.MapTypeId.ROADMAP }; //thuộc tính của bản đồ
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    var marker = new google.maps.Marker({ position: myCenter });
    marker.setMap(map);
}

function googleTranslate() {
    new google.translate.TranslateElement(
        {
            pageLanguage: 'vi',
            includedLanguages: 'vi,en,it,la,fr',
            //layout: google.translate.TranslateElement.InlineLayout.SIMPLE
        },
        'google_translate');
}

$.getScript('//translate.google.com/translate_a/element.js?cb=googleTranslate');
$.getScript('https://maps.googleapis.com/maps/api/js?key=AIzaSyDzJLrq30qhSANk6gPyMsTx9nwaJ6JWi3s');

