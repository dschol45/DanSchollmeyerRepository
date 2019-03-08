$(document).ready(function() {
  LoadItems();

});

//Load Items
function LoadItems() {
$.ajax({
  type: 'GET',
  url: 'http://localhost:8080/items',
  success: function(data,status) {
    // alert('stuff')
    $.each(data, function(index,item) {
      var id = item.id;
      var name = item.name;
      var price = item.price;
      var quantity = item.quantity;
      var num = index+1;
      //update to use index+1 instead of id
      $('#itemNumber'+num).text(id);
      $('#itemName'+num).text(name);
      $('#itemAmount'+num).text('$ '+price);
      $('#itemQuantity'+num).text('Quantity Left: '+quantity);
    });
  },
error: function(){
  alert('Failed to Load Items');
},
});}

//update Total $ In area//
var money = 0.00; 

$('#addDollar').on('click', function() {
  money+=1.00;
   updateMoney();});
   
$('#addQuarter').on('click', function() {
money+=0.25;
updateMoney();});

$('#addDime').on('click',function(){
  money+=0.10;
  updateMoney();});
         
   function updateMoney(){
$('#moneyBox').val( money.toFixed(2));};

$('#addNickel').on('click', function() {
  money+=0.05;
  updateMoney();});
         
   function updateMoney(){
$('#moneyBox').val( money.toFixed(2));};

//add item info
$('.itemButton').on('click', function() {
  var id = this.value;
  var itemInfo = this.value + ': ' + $('#itemName'+id).text();
  $('#itemBox').val(itemInfo);
});

//click make purchase
  //if success, reduce quantity, total - item price to change,display Thank You message, total in to 0 (check assignment reqs)
  //if stock=0, error message (check assignment reqs)
  //if not enough money, error message (check assignment reqs)

$(document).on('click','#makePurchase',function(){
// $('#makePurchase').on('click',function(){
  var id = $('#itemBox').val().substring(0,1);
  var moneyIn = $('#moneyBox').val();
  var change = 0.00;
  //alert(id+', '+moneyIn);
  $('#changeBox').val(0);

  $.ajax({
    type: 'GET',
    url: 'http://localhost:8080/money/'+moneyIn+'/item/'+id,
    
    success: function(item){
        var quartersChange = item.quarters*.25;
        var dimeChange = item.dimes*.1;
        var nickelChange = item.nickels*.05;
        var change = quartersChange+dimeChange+nickelChange;
        money=0;
        $('#changeBox').val('Change: '+change+' / '+item.quarters+' Quarters : '+item.dimes+' Dimes : '+item.nickels+' Nickels');
        $('#moneyBox').val(money);
        $('#messageBox').val('Thank You!!!');
        LoadItems();
    },
    error: function(jqXHR){
      $('#messageBox').val(JSON.parse(jqXHR.responseText).message);
    }
  });
})

//return change
$(document).on('click','#changeReturn',function(){
  var change = money;
  money=0;

  $('#moneyBox').val(money);
  $('#itemBox').val('');
  $('#messageBox').val('')

  var balance = Math.round((change*100));
  quarters = Math.floor(balance/25);
  balance %= 25;
  dimes = Math.floor(balance/10);
  balance %= 10;
  nickels = Math.floor(balance/5);
  balance = 0;
  $('#changeBox').val('Change: '+change+' / '+quarters+' Quarters : '+dimes+' Dimes : '+nickels+' Nickels');
  change=0;
});

// $('#makePurchase').on('click',function(){
//   var id = $('#itemBox').val().substring(0,1);
//   var cost = $('#itemAmount'+id).text().substring(2);
//   var moneyIn = $('#moneyBox').val();
//   var change = 0.00;
//   // alert( id+ ': ' + cost+ ': ' + moneyIn);
//   var qty = ($('#itemQuantity'+id).text().substring(15)*1)-1;
//   alert(qty);
//   if(qty <= -1)
//   {
//     $('#messageBox').val('SOLD OUT!');
//   };
//   if(qty>=0)
//   {
//     if(cost > moneyIn)
//     { 
//     change = cost - moneyIn;
//     alert('Please Insert: '+change);
//     change=0.00;
//     };
//     if(moneyIn >= cost)
//     {
//     change = (moneyIn-cost).toFixed(2);
//     //$('#changeBox').val(change);
//     $('#moneyBox').val(change);
//     $('#itemBox').val('');
//     $('#messageBox').val('Thank You!');
//     $('#itemQuantity'+id).text('Quantity left: '+qty);
//     };
//   };
// });