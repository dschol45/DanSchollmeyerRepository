$(document).ready(function() {
  $('#currentDiv').hide();
  $('#forecastDiv').hide();
  $('#edit-form').on('submit',SubmitWeather);
});

function SubmitWeather(event){
  event.preventDefault();
  //alert($('#getZipcode').val());
  var zipcode = $('#getZipcode').val();
  var units = $('#unitsChoice').val();
  
  var tempType;
  if (units=='Imperial') {
    tempType = 'Farenheit';
  }
  if (units=='Metric') {
    tempType = 'Celcius';
  }

  var windType;
  if (units == 'Imperial') {
    windType = 'Miles/Hour';
  }
  if (units == 'Metric') {
    windType = 'Meters/Sec';
  }

  $.ajax ({
      type: 'GET',
      url: 'http://api.openweathermap.org/data/2.5/weather?zip='+zipcode+',us&APPID=d783dbbd98c97a52a5540bb9376e69e2&units='+units,
      success: function (data, status) {
          //alert(data.weather[0].description);
          $('#currentHeader').text('Current Conditions in '+data.name)
          $('#currentIMG').text('').append('<img src="http://openweathermap.org/img/w/'+ data.weather[0].icon + '.png"/>');
          $('#currentDescription').text(data.weather[0].description);
          $('#currentTemperature').text('Temperature:  ' + data.main.temp + '  ' + tempType);//add text for units being used
          $('#currentHumidity').text('Humidity:  ' + data.main.humidity+'%');
          $('#currentWind').text('Wind:  ' + data.wind.speed + '  ' + windType);

      },
      error: function() {
          alert("no u");
      }
  });
  $('#currentDiv').show();
  $('#forecastDiv').show();
  //if #getZipcode returns weather info, #currentDiv.show() and #forecastDiv.show()

}
