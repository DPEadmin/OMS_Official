<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectBranch.ascx.cs" Inherits="DOMS_TSR.src.UserControl.SelectBranch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<script type="text/javascript">

    var clipboard = new ClipboardJS('#copyaddress');
 
    function AppearSearch() {
        document.getElementById('pac-input').style.display = 'inline';
        document.getElementById('searchclear').style.display = 'inline';
    }

    function HideSearch() {
        document.getElementById('pac-input').style.display = 'none';
        document.getElementById('searchclear').style.display = 'none';
    }

    function CopyAddress() {
        var dummyLink = $(AddressCopy).val();
        var dummy = $('<input>').val(dummyLink).appendTo('body').select();
        dummy.focus();
        document.execCommand("copy");

        dummy.hide();

        //alert("Copied the text: " + dummyLink);
    }

    function ClearSearch() {
        count = 1;
        if (document.getElementById("<%= hidFlagCusLatLng.ClientID %>").value == "TRUE") {
            document.getElementById("<%= hidNearLat.ClientID %>").value = "";
            document.getElementById("<%= hidNearLng.ClientID %>").value = "";
            document.getElementById("Address").value = "";
            document.getElementById("pac-input").value = "";  
            initialize();
        }else {
            document.getElementById("<%= hidCustomerLat.ClientID %>").value = "";
            document.getElementById("<%= hidCustomerLng.ClientID %>").value = "";
            document.getElementById("<%= hidNearLat.ClientID %>").value = "";
            document.getElementById("<%= hidNearLng.ClientID %>").value = "";
            document.getElementById("Address").value = "";
            document.getElementById("pac-input").value = "";  
            initialize();
        }
           
            //bermudaTriangle.setMap(null);
            //autocomplete.set('place', null);
        

    }

    function ClearMarker() {
         for(i=0; i<gmarkers.length; i++){
                gmarkers[i].setMap(null);
         }
    }

    function ShowInfoWindows() {
        if (zoomLevel >= 15) {
            for (i = 0; i < infoWindows.length; i++) {
                infoWindows[i].close();
            }
            for (i = 0; i < gmarkers.length; i++) {
                 google.maps.event.trigger(gmarkers[i], 'click')
            }
        }
}


    function DrawPolygon() {
      
       var obj = {};
        obj.currentLat = document.getElementById("<%= hidCustomerLat.ClientID %>").value;
        obj.currentLng = document.getElementById("<%= hidCustomerLng.ClientID %>").value;

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TakeOrder.aspx/BindPolygon",
            data: JSON.stringify(obj),
                dataType: "json",
                success: function (data) 
                {
                    //alert("Draw Polygon");
                      document.getElementById("<%= hidAreaCode.ClientID %>").value = data.d[0].AreaCode;
                      data.d.splice(0,1);

             
                     var triangleCoords = [ data.d ];
                    // Construct the polygon.
                        bermudaTriangle = new google.maps.Polygon({
                        paths: triangleCoords,
                            strokeColor: '#F2B1F4',
                        strokeOpacity: 0.6,
                        strokeWeight: 1,
                            fillColor: '#F45048',
                        fillOpacity: 0.35
                    });
                    bermudaTriangle.setMap(map);  
                    DrawMapMarker();
 
                },	
                error: function (result) {
                    alert(result.responseText);
                },
        });      

           
    }
     

    function DrawMapMarker() {
        var obj1 = {};
           obj1.AreaCode = document.getElementById("<%= hidAreaCode.ClientID %>").value;

         $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "TakeOrder.aspx/BindMapMarker",
                data: JSON.stringify(obj1),
                dataType: "json",
                success: function (data) 
                {
                 
                    var ltlng = [];
                    gmarkers = [];
                    

                    for (var i = 0; i <= data.d.length; i++) {

                        ltlng.push(new google.maps.LatLng(data.d[i].Latitude, data.d[i].Longitude));
                        var icon = {
                          
                            scaledSize: new google.maps.Size(50, 50), // scaled size
                            origin: new google.maps.Point(0,0), // origin
                            anchor: new google.maps.Point(0, 0) // anchor
                        }

                        marker = new google.maps.Marker({
                            map: map,
                            //animation: google.maps.Animation.DROP,
                            icon: icon,
                            scaledSize: new google.maps.Size(50, 50), // scaled size
                            origin: new google.maps.Point(0,0), // origin
                            anchor: new google.maps.Point(0, 0), // anchor
                            title: data.d[i].LocationName,
                            position: ltlng[i]
                        });
                        (function (marker, i) {

                            google.maps.event.addListener(marker, 'click', function () {
                                 
                                      
                                infowindow = new google.maps.InfoWindow({ maxWidth: 250,disableAutoPan: true });
                                infowindow.setContent(data.d[i].LocationName);
                                infowindow.open(map, marker);

                                infoWindows.push(infowindow);
                                //google.maps.event.addListener(infowindow, 'closeclick', function() {
                                   
                                //    if (marker.open == false) {
                                //        infowindow.open(map, marker);
                                //        marker.open = true;
                                //    }
                                //    else{
                                //        infowindow.close();
                                //        marker.open = false;
                                //    }
                                //});

                                //if (marker.open == undefined) {
                                //    marker.open = false;
                                //} 
                               
                             
                                //if(marker.open == false){
                                //    infowindow.open(map, marker);
                                //    marker.open = true;
                                //}
                                //else {
                                //    infowindow.close(map, marker);
                                //    marker.open = false;
                                //}
                                   
                             
                            });



                        })(marker, i);
                        gmarkers.push(marker);
                    
                    }
                 
                },
                error: function (result) {
                    alert(result.responseText);
                }
            });
    }


</script>

<style type="text/css">
#locationField, #controls {
    position: relative;
    width: 480px;
    }
/* css สำหรับ div คลุม google map อีกที */
#contain_map{
	position:relative;
	width:650px;
	height:400px;
	margin:auto;	
}	
/* css กำหนดความกว้าง ความสูงของแผนที่ */
#map_canvas { 
	top:0px;
	width:100%;
	height:400px;
	margin:auto;
}
.pac-container {
  z-index: 1050 !important;
}
/*css กำหนดรูปแบบ ของ input สำหรับพิมพ์ค้นหา effect */
.controls_tools {
	margin-top: 16px;
	border: 1px solid transparent;
	border-radius: 2px 0 0 2px;
	box-sizing: border-box;
	-moz-box-sizing: border-box;
	height: 32px;
	outline: none;
	box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
    z-index: 1051 !important;
}
/*css กำหนดรูปแบบ ของ input สำหรับพิมพ์ค้นหา*/
#pac-input {
	background-color: #fff;
	padding: 0 11px 0 13px;
	width: 60%;
	font-family: Roboto;
	font-size: 15px;
	font-weight: 300;
	text-overflow: ellipsis;

}
/*css กำหนดรูปแบบ ของ input สำหรับพิมพ์ค้นหา ขณะ focus*/
#pac-input:focus {
	width: 60%;
	border-color: #4d90fe;
	margin-left: -1px;
	padding-left: 14px;  /* Regular padding-left + 1. */      
}

</style>
 
<script type="text/javascript">
var infoWindows = [];
var geocoder; 
var map; 
var my_Marker; 
var cus_Marker;
var GGM; 
var inputSearch; 
var infowindow;
var autocomplete; 
var bermudaTriangle
var gmarkers = [];
var count = 1;
var cusLat;
var cusLng;
var zoomLevel;
var zoomcount = 0;
var dragcount = 0;
function initialize() { // ฟังก์ชันแสดงแผนที่
	GGM=new Object(google.maps); // เก็บตัวแปร google.maps Object ไว้ในตัวแปร GGM
	geocoder = new GGM.Geocoder(); // เก็บตัวแปร google.maps.Geocoder Object
    //get customer Lat Lng
    cusLat = document.getElementById("<%= hidCustomerLat.ClientID %>").value;
    cusLng = document.getElementById("<%= hidCustomerLng.ClientID %>").value;
    
    // กำหนดจุดเริ่มต้นของแผนที่
    if (cusLat != "" && cusLng != "") {
       var my_Latlng  = new GGM.LatLng(cusLat,cusLng);
    }else {
       var my_Latlng  = new GGM.LatLng(13.761728449950002,100.6527900695800);
    }

	var my_mapTypeId=GGM.MapTypeId.ROADMAP; // กำหนดรูปแบบแผนที่ที่แสดง
	// กำหนด DOM object ที่จะเอาแผนที่ไปแสดง ที่นี้คือ div id=map_canvas
	var my_DivObj=$("#map_canvas")[0];
	// กำหนด Option ของแผนที่
	var myOptions = {
		zoom: 13, // กำหนดขนาดการ zoom
        streetViewControl: false, //disable street view
		center: my_Latlng , // กำหนดจุดกึ่งกลาง จากตัวแปร my_Latlng
		mapTypeId:my_mapTypeId // กำหนดรูปแบบแผนที่ จากตัวแปร my_mapTypeId
	};
	map = new GGM.Map(my_DivObj,myOptions); // สร้างแผนที่และเก็บตัวแปรไว้ในชื่อ map

	inputSearch = $("#pac-input")[0]; // เก็บตัวแปร dom object โดยใช้ jQuery
	// จัดตำแหน่ง input สำหรับการค้นหา ด้วย คำสั่งของ google map
	//map.controls[GGM.ControlPosition.TOP_LEFT].push(inputSearch);
	
	// เรียกใช้งาน Autocomplete โดยส่งค่าจากข้อมูล input ชื่อ inputSearch
	autocomplete = new GGM.places.Autocomplete(inputSearch);
	autocomplete.bindTo('bounds', map); 
	
	infowindow = new GGM.InfoWindow();// เก็บ InfoWindow object ไว้ในตัวแปร infowindow
	// เก็บ Marker object พร้อมกำหนดรูปแบบ ไว้ในตัวแปร my_Marker
	my_Marker = new GGM.Marker({
        map: map,
        draggable:true,
		anchorPoint: new GGM.Point(0, -29)
    });

     
	//my_Marker.setIcon(/** // กำหนดรูปแบบของ icons การแสดงสถานที่ */({
	//	url: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png",
	//	size: new GGM.Size(71, 71),
	//	origin: new GGM.Point(0, 0),
	//	anchor: new GGM.Point(20, 44),
	//	scaledSize: new GGM.Size(40, 40)
 //   }));

    cus_Marker = new GGM.Marker({
        map: map,
        draggable:true,
		anchorPoint: new GGM.Point(0, -29)
    });

    cus_Marker.setIcon(/** // กำหนดรูปแบบของ icons การแสดงสถานที่ */({
		url: "/homePin.png",
        size: new GGM.Size(71, 71),
		origin: new GGM.Point(0, 0),
		anchor: new GGM.Point(17, 40),
		scaledSize: new GGM.Size(35, 35)
    }));


    if (cusLat != "" && cusLng != "") {
        cusLatLng = new google.maps.LatLng(cusLat, cusLng);
        //cus_Marker.draggable = false;
        cus_Marker.setPosition(cusLatLng);

        map.panTo(cusLatLng);
    
        //infowindow = new google.maps.InfoWindow({ maxWidth: 250 });
        //infowindow.setContent("ที่อยู่ลูกค้า");
        //infowindow.open(map, cus_Marker);

        DrawPolygon();
       

    }

    google.maps.event.addListener(map, 'zoom_changed', function () {
        zoomLevel = map.getZoom();
       
        if (zoomLevel >= 15) {
            if (zoomcount == 0) {
                for (i = 0; i < infoWindows.length; i++) {
                    infoWindows[i].close();
                    
                }
                for (i = 0; i < gmarkers.length; i++) {
                    google.maps.event.trigger(gmarkers[i], 'click')
                }
                zoomcount++;
            } else {
               zoomcount++;
            }    
        } else {
            zoomcount = 0;
            for (i = 0; i < infoWindows.length; i++) {
                infoWindows[i].close();
            }
                
        }
     });


      // กำหนด event ให้กับตัว marker เมื่อสิ้นสุดการลากตัว marker ให้ทำงานอะไร
      GGM.event.addListener(my_Marker, 'dragend', function () {


        document.getElementById("<%= hidNearLat.ClientID %>").value = my_Marker.getPosition().lat();
        document.getElementById("<%= hidNearLng.ClientID %>").value =  my_Marker.getPosition().lng();
          
        var my_Point = my_Marker.getPosition();  // หาตำแหน่งของตัว marker เมื่อกดลากแล้วปล่อย

        map.panTo(my_Point);  // ให้แผนที่แสดงไปที่ตัว marker       
         
        // เรียกขอข้อมูลสถานที่จาก Google Map
        geocoder.geocode({'latLng': my_Point}, function(results, status) {
          if (status == GGM.GeocoderStatus.OK) {
              if (results[1]) {
                // แสดงข้อมูลสถานที่ใน textarea ที่มี id เท่ากับ place_value
                 $("#Address").val(results[1].formatted_address); // 
                 $("#pac-input").val(results[1].formatted_address); // 
            }
          } else {
              // กรณีไม่มีข้อมูล
            alert("Geocoder failed due to: " + status);
          }
        });     

    }); 



     // กำหนด event ให้กับตัว marker เมื่อสิ้นสุดการลากตัว marker ให้ทำงานอะไร
     GGM.event.addListener(cus_Marker, 'dragend', function () {
        count++;

        cusLat = cus_Marker.getPosition().lat();
        cusLng = cus_Marker.getPosition().lng();

        document.getElementById("<%= hidCustomerLat.ClientID %>").value = cus_Marker.getPosition().lat();
        document.getElementById("<%= hidCustomerLng.ClientID %>").value = cus_Marker.getPosition().lng();
          
        var my_Point = cus_Marker.getPosition();  // หาตำแหน่งของตัว marker เมื่อกดลากแล้วปล่อย

        map.panTo(my_Point);  // ให้แผนที่แสดงไปที่ตัว marker       

         if (count > 1) {

            bermudaTriangle.setMap(null);
            ClearMarker();

            DrawPolygon();

             ShowInfoWindows();
             
  
        }else {

            DrawPolygon();
        }
        
         

         
    }); 





	// เมื่อแผนที่มีการเปลี่ยนสถานที่ จากการค้นหา
    GGM.event.addListener(autocomplete, 'place_changed', function () {
        count++;
      
		//infowindow.close();// เปิด ข้อมูลตัวปักหมุด (infowindow)
		my_Marker.setVisible(false);// ซ่อนตัวปักหมุด (marker) 
		var place = autocomplete.getPlace();// เก็บค่าสถานที่จากการใช้งาน autocomplete ไว้ในตัวแปร place
        if (!place.geometry) {// ถ้าไม่มีข้อมูลสถานที่ 
             document.getElementById("pac-input").value = "";
             document.getElementById("Address").innerText = "";
            alert("ไม่พบสถานที่ที่ต้องการค้นหา")
			return;
		}
		
		// ถ้ามีข้อมูลสถานที่  และรูปแบบการแสดง  ให้แสดงในแผนที่
		if (place.geometry.viewport) {
			map.fitBounds(place.geometry.viewport);
		} else { // ให้แสดงแบบกำหนดเอง
            map.setCenter(place.geometry.location);
            map.setZoom(zoomLevel);  // แผนที่ขยายที่ขนาด 17 ถือว่าเหมาะสม
        }

        //alert(place.geometry.location.lat() + " " + place.geometry.location.lng()) เอาค่า lat long ๑๑๑๑๑๑๑
        document.getElementById("<%= hidNearLat.ClientID %>").value = place.geometry.location.lat();
        document.getElementById("<%= hidNearLng.ClientID %>").value = place.geometry.location.lng();

        // ปักหมุด (marker) Customer

<%--        if (cusLat != "" && cusLng != "") {
            cusLatLng = new google.maps.LatLng(cusLat, cusLng);
            cus_Marker.setPosition(cusLatLng);
            bermudaTriangle.setMap(null);
            ClearMarker();
            DrawPolygon();
    
        } else {  
               
            cusLatLng = new google.maps.LatLng(cusLat, cusLng);
            //cus_Marker.draggable = true;
            cus_Marker.setPosition(place.geometry.location);
            cus_Marker.setVisible(true);// แสดงตัวปักหมุด จากการซ่อนในการทำงานก่อนหน้า

            document.getElementById("<%= hidCustomerLat.ClientID %>").value = place.geometry.location.lat();
            document.getElementById("<%= hidCustomerLng.ClientID %>").value = place.geometry.location.lng();

            if (count > 2) {
              bermudaTriangle.setMap(null);
              ClearMarker();
            }

            DrawPolygon();
       
        }--%>
    
        if (cusLat == "" && cusLng == "") {
            cusLatLng = new google.maps.LatLng(cusLat, cusLng);
            cus_Marker.setPosition(place.geometry.location);
            cus_Marker.setVisible(true);// แสดงตัวปักหมุด จากการซ่อนในการทำงานก่อนหน้า

            document.getElementById("<%= hidCustomerLat.ClientID %>").value = place.geometry.location.lat();
            document.getElementById("<%= hidCustomerLng.ClientID %>").value = place.geometry.location.lng();

            //if (count > 2) {
            //  bermudaTriangle.setMap(null);
            //  ClearMarker();
            //}

            DrawPolygon();

        }
      

        // ปักหมุด (marker) ตำแหน่ง สถานที่ที่เลือก
        var newLat = place.geometry.location.lat();
        var newLng = place.geometry.location.lng();
        newLat += 0.00025000;
        var newLatLng = new google.maps.LatLng(newLat, newLng);

        my_Marker.setPosition(newLatLng);
        my_Marker.setVisible(true);// แสดงตัวปักหมุด จากการซ่อนในการทำงานก่อนหน้า

        
        //my_Marker.setAnimation(google.maps.Animation.BOUNCE);


		// สรัางตัวแปร สำหรับเก็บชื่อสถานที่ จากการรวม ค่าจาก array ข้อมูล
        var address = '';
        var addresscopy = '';
		if (place.address_components) {
			address = [
				(place.address_components[0] && place.address_components[0].short_name || ''),
				(place.address_components[1] && place.address_components[1].short_name || ''),
                (place.address_components[2] && place.address_components[2].short_name || ''),
                (place.address_components[3] && place.address_components[3].short_name || ''),
				(place.address_components[4] && place.address_components[4].short_name || ''),
                (place.address_components[5] && place.address_components[5].short_name || ''),
                (place.address_components[6] && place.address_components[6].short_name || ''),
                (place.address_components[7] && place.address_components[7].short_name || ''),
            ].join(' ');

            addresscopy = [
                (place.address_components[0] && place.address_components[0].short_name || ''),
			
            ].join(' ');
        }

        document.getElementById("Address").value = place.name + ' ' +address;
         document.getElementById("AddressCopy").value = place.name + ' ' + addresscopy;
		
		// แสดงข้อมูลในตัวปักหมุด (infowindow)
		//infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);
        //infowindow.open(map, my_Marker);// แสดงตัวปักหมุด (infowindow)
        ShowInfoWindows();
		
	});


    }
    $(function(){
    // โหลด สคริป google map api 
    $("<script/>", {
      "type": "text/javascript",
        src: "https://maps.google.com/maps/api/js?v=3.2&key=AIzaSyCtfKs5LsP1Yb4TR7gxdkjtrEEFEgedSII&sensor=false&language=th&callback=initialize&libraries=places,drawing,geometry"
    }).appendTo("body");    
});

</script>


    <!-- edit Address modal start -->
  <!-- Large modal -->

  <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" id="modal-address" 
    aria-hidden="true" >
    <div class="modal-dialog modal-lg" style="max-width:900px;">
      <div class="modal-content">
        <!-- Basic Form Inputs card start -->

           <div class="card " style="margin-bottom: 0px !important;">
          <div class="card-header">

            <h5 class="modal-title sub-title">ที่อยู่จัดส่ง</h5>
          <button type="button" class="close " style="font-size:1.2rem;" data-dismiss="modal" aria-label="Close"> <span aria-hidden="true">&times;</span> </button>
          </div>
          <div class="modal-body">
            <div class="form-group row">
            <label class="col-sm-3 col-form-label">ค้นหาสถานที่ใกล้เคียง</label>
            <div class="col-sm-9 col-form-label">
             <input style="width:80%;margin-top:-5px;" id="pac-input" class="controls_tools" type="text" placeholder="ค้นหาข้อมูลที่อยู่">

             <span id="searchclear"onclick="ClearSearch()" class="form-contorl btn-five btn-clear">Clear</span>
             <span id="copyaddress"onclick="CopyAddress()" class="form-contorl btn-five btn-clear" data-clipboard-action="copy" data-clipboard-target=#AddressCopy>Copy</span>


            
             <input type="hidden" name="lat" id="lat">
             <input type="hidden" name="long" id="long">

            </div>
            <div class="col-sm-12">
                 <div id="map_canvas"></div>
            </div>
            <label class="m-t-15 col-sm-12"></label>
            <label class="col-sm-3 col-form-label" style="display:none">ที่อยู่ใกล้เคียง</label>
             <div class="col-sm-9" >
             <textarea style="padding-left:40px;background-color:white;width: 100%;display:none" id="Address" rows="3" cols="30" disabled ></textarea>

         <div style="position:absolute ; top : 0;Z-index :-999999;">
              <input type="text" id="AddressCopy">
        </div>
                
           
                 </div>
             </div>

          
        

         <asp:UpdatePanel id="AddressModal" runat="server">
                <ContentTemplate>
                <asp:HiddenField ID="hidAreaCode" Value="00" runat="server" />
                <asp:HiddenField ID="hidFlagCusLatLng" runat="server" />
                <asp:HiddenField ID="hidEmpCode" runat="server" />
                <asp:HiddenField ID="hidFlagInsert" runat="server" />
                <asp:HiddenField ID="hidEditCustomerAddressId" runat="server" />
                <asp:HiddenField ID="currentDeliveryAddressId" runat="server" />
                <asp:HiddenField ID="currentReceiptAddressId" runat="server" />
                <asp:HiddenField ID="hidCustomerLat" runat="server" />
                <asp:HiddenField ID="hidCustomerLng" runat="server" />
                <asp:HiddenField ID="hidNearLat" runat="server" />
                <asp:HiddenField ID="hidNearLng" runat="server" />
                <asp:HiddenField ID="currentDeliveryAddress" runat="server" />
                <asp:HiddenField ID="currentDeliveryDistrict" runat="server" />
                <asp:HiddenField ID="currentDeliveryDistrictName" runat="server" />
                <asp:HiddenField ID="currentDeliverySubDistrict" runat="server" />
                <asp:HiddenField ID="currentDeliverySubDistrictName" runat="server" />
                <asp:HiddenField ID="currentDeliveryProvince" runat="server" />
                <asp:HiddenField ID="currentDeliveryProvinceName" runat="server" />
                <asp:HiddenField ID="currentDeliveryZipCode" runat="server" />
                <asp:HiddenField ID="currentReceiptAddress" runat="server" />
                <asp:HiddenField ID="currentReceiptDistrict" runat="server" />
                <asp:HiddenField ID="currentReceiptDistrictName" runat="server" />
                <asp:HiddenField ID="currentReceiptSubDistrict" runat="server" />
                <asp:HiddenField ID="currentReceiptSubDistrictName" runat="server" />
                <asp:HiddenField ID="currentReceiptProvince" runat="server" />
                <asp:HiddenField ID="currentReceiptProvinceName" runat="server" />
                <asp:HiddenField ID="currentReceiptZipCode" runat="server" />
            <%--    <div class="form-group row"> 
                <label class="col-sm-3 col-form-label">ประเภทที่อยู่</label>
                <div class="col-sm-3">
                <asp:DropDownList ID="ddlAddressType" OnSelectedIndexChanged="ddlAddressType_SelectedIndexChanged"  AutoPostBack="true" runat="server"  CssClass="form-control"></asp:DropDownList>
                </div>
                </div>
                <div class="form-group row"> 
                <div class="col-sm-12 m-t-15">   
                <div class="card-block" style="border:1px solid gray;" >
                <div class="row">
                    <div class="col-sm-6">
                        <asp:Label ID="lblFullAddress" runat="server" />
                    </div>
                    <div class="col-sm-6" style="text-align:right" >
                  <asp:Button ID="btnAddAddress" Text="เพิ่ม" OnClick="btnAddAddress_Click"
                      class="btn-mk-pri btn-addmap m-r-10"
                      runat="server" />
                        
                  <asp:Button ID="btnSelectAddress" Text="เลือก" OnClick="btnSelectAddress_Click"
                      class="btn-mk-pri btn-selectmap m-r-10"
                      runat="server" />

                        
                  <asp:Button ID="btnEditAddress" Text="แก้ไข" OnClick="btnEditAddress_Click"
                      class="btn-mk-pri btn-edit m-r-10"
                      runat="server" />
                    </div>
                </div>
                    </div>
                         </div>
                             </div>--%>
               <asp:HiddenField ID="hidAddressType" runat="server" />
                    <div class="col-sm-12 p-0" style="display:flex;  ">
                        <div class="col-sm-6  p-0" style="padding-right:5px !important" >
                            <div class="p-b-5">ที่อยู่จัดส่ง </div> 

                            <div style=" border: 1px solid gray;">
                                 <div class="card-block" style="  position: relative; min-height: 13vh; ">
                <div><asp:Label ID="lblFullAddress" runat="server" /></div>      
                <div class="col-sm-12 text-center " style="  position: absolute;bottom: 10px;">
                  <asp:Button ID="btnAddAddress" Text="เพิ่ม" OnClick="btnAddAddress_Click" runat="server"  class="btn-mk-pri btn-addmap m-r-10" />
                
                  <asp:Button  ID="btnSelectAddress" Text="เลือก" OnClick="btnSelectAddress_Click" runat="server" class="btn-mk-pri btn-selectmap m-r-10"/>
                
                  <asp:Button ID="btnEditAddress" Text="แก้ไข" OnClick="btnEditAddress_Click" runat="server" class="btn-mk-pri btn-edit " />
                </div>
                </div>
                            </div>
                        </div>
                        <div class="col-sm-6  p-0" style="padding-left:5px !important" > 
                            <div class="p-b-5"> ที่อยู่ใบกำกับภาษี </div> 
                            <div style=" border: 1px solid gray;">
                                <div class="card-block" style="position: relative; min-height: 13vh;">
                                    <div>
                                        <asp:CheckBox ID="chkAddress" Checked="true" OnCheckedChanged="chkAddress_Change" AutoPostBack="true" runat="server" />ที่อยู่เดียวกับที่จัดส่ง</div>
                                    <div>
                                        <asp:Label ID="lblFullAddress1" runat="server" /></div>
                                    <div class="col-sm-12 text-center " style="position: absolute; bottom: 10px;">
                                        <asp:Button ID="btnAddAddress1" Text="เพิ่ม" OnClick="btnAddAddress1_Click" runat="server" class="btn-mk-pri btn-addmap m-r-10" />

                                        <asp:Button ID="btnSelectAddress1" Text="เลือก" OnClick="btnSelectAddress1_Click" runat="server" class="btn-mk-pri btn-selectmap m-r-10" />

                                        <asp:Button ID="btnEditAddress1" Text="แก้ไข" OnClick="btnEditAddress1_Click" runat="server" class="btn-mk-pri btn-edit " />
                                    </div>
                                </div>
                            </div>

                        </div>
                         
                    </div>
                
         

           

            <%--   <div class="col-sm-12 p-l-0 p-r-0 p-t-20">
               <table class="mapchoice ">

               <thead>
               <tr>
               <th style="width:1%"></th>
               <th style=" text-align:left;"> ที่อยู่</th>
             
               </tr>
               </thead>
               <tbody>
               <tr>
                <td style="white-space: nowrap;width: 1%;padding: 5 88px  5px;padding-bottom: 3px;padding-top: 3px;">
               <button class="button-pri button-activity  button-activity ">เลือก</button>
                </td> 
               <td>333 ท่าแร้ง เขตบางเขน กรุงเทพมหานคร 10200</td>
               </tr>
                <tr>
                <td style="white-space: nowrap;width: 1%;padding: 5 88px  5px;padding-bottom: 3px;padding-top: 3px;">
               <button class="button-pri button-activity  button-activity ">เลือก</button>
                </td> 
               <td>333 ท่าแร้ง เขตบางเขน กรุงเทพมหานคร 10200</td>
               </tr>
                <tr>
                <td style="white-space: nowrap;width: 1%;padding: 5 88px  5px;padding-bottom: 3px;padding-top: 3px;">
               <button class="button-pri button-activity  button-activity ">เลือก</button>
                </td> 
               <td>333 ท่าแร้ง เขตบางเขน กรุงเทพมหานคร 10200</td>
               </tr>
               </tbody>
               </table>
               </div>--%>

               
              
              


         <div id="SelectAddressSection" runat="server">
           
                <asp:GridView ID="gvAddress" runat="server" AutoGenerateColumns="False" CssClass="mapchoice"
                            TabIndex="0" Width="100%" CellSpacing="0" OnRowCommand="gvAddress_RowCommand"
                            ShowHeaderWhenEmpty="true">

                            <Columns>
                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%" HeaderStyle-CssClass="" ItemStyle-CssClass="">
                                    <HeaderTemplate>
                               
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                          <asp:LinkButton ID="btnEdit" runat="Server" CommandName="SelectAddress"
                                          class="button-pri button-activity  button-activity" 
                                          CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">เลือก</asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>

            

                                <asp:TemplateField  HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="80%" HeaderStyle-CssClass="" ItemStyle-CssClass="">

                                    <HeaderTemplate>

                                        <div style="padding-left:17px;">ที่อยู่</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                       
                                          &nbsp;&nbsp;&nbsp;<asp:Label ID="lblAddress" Text='<%# DataBinder.Eval(Container.DataItem, "Address")%>' runat="server" />
                                          &nbsp;<asp:Label ID="lblSubDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "SubdistrictName")%>' runat="server" />  
                                          &nbsp;<asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictName")%>' runat="server" />
                                          &nbsp;<asp:Label ID="lblProvince" Text='<%# DataBinder.Eval(Container.DataItem, "ProvinceName")%>' runat="server" />
                                          &nbsp;<asp:Label ID="lblPostCode" Text='<%# DataBinder.Eval(Container.DataItem, "ZipCode")%>' runat="server" />

                                         <asp:HiddenField runat="server" ID="hidCustomerAddressId" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerAddressId")%>' />
                                    
                                    </ItemTemplate>

                                </asp:TemplateField>

                               <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">ตำบล / แขวง</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblSubDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "SubdistrictName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                 <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">อำเภอ / เขต</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblDistrict" Text='<%# DataBinder.Eval(Container.DataItem, "DistrictName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">จังหวัด</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblProvince" Text='<%# DataBinder.Eval(Container.DataItem, "ProvinceName")%>' runat="server" />

                                    </ItemTemplate>

                                </asp:TemplateField>

                                  <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="400px" HeaderStyle-CssClass="TDHead" ItemStyle-CssClass="TDDetail">

                                    <HeaderTemplate>

                                        <div align="center">รหัสไปรษณีย์</div>

                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        &nbsp;&nbsp;<asp:Label ID="lblPostCode" Text='<%# DataBinder.Eval(Container.DataItem, "ZipCode")%>' runat="server" />

                                         <asp:HiddenField runat="server" ID="hidCustomerAddressId" Value='<%# DataBinder.Eval(Container.DataItem, "CustomerAddressId")%>' />
                                    </ItemTemplate>

                                </asp:TemplateField>
                                   --%>
                              
                            </Columns>

                            <EmptyDataTemplate>
                                <center>
                                    <asp:Label ID="lblDataEmpty" class="fontBlack" runat="server" Text="Data not Found"></asp:Label>
                                </center>
                            </EmptyDataTemplate>
                        </asp:GridView>

           
           </div>
            <!-- address table end -->

            <!-- insert address section start -->
            <div id="InsertSection"  runat="server">
            <div class="card-block m-t-20" style="border: 1px solid">
                <div class="form-group row" >
                 <div class="card-header" style="border:none;"><h5 class="f-14 sub-title" ><asp:Label ID="lblAddressText" runat="server" ></asp:Label></h5></div>
                  <label class="col-sm-2 col-form-label">ที่อยู่</label>
                  <div class="col-sm-9">
                     <asp:TextBox id="txtAddress_Ins" class=" form-control form-controls" runat="server"></asp:TextBox>
                     <asp:Label ID="lblAddress" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-2 col-form-label">จังหวัด</label>
                  <div class="col-sm-4">
                     <asp:DropDownList ID="ddlProvince" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged"  AutoPostBack="true" runat="server" class="form-control form-controls"></asp:DropDownList>
                     <asp:Label ID="lblProvince_Ins" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                 
                  <label class="col-sm-2 col-form-label">เขต/อำเภอ</label>
                  <div class="col-sm-4">
                      <asp:DropDownList ID="ddlDistrict" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true" runat="server" class="form-control form-controls"></asp:DropDownList>
                      <asp:Label ID="lblDistrict_Ins" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-2 col-form-label">แขวง/ตำบล</label>
                  <div class="col-sm-4">
                    <asp:DropDownList ID="ddlSubDistrict" runat="server" cssclass="form-control form-controls"></asp:DropDownList>
                    <asp:Label ID="lblSubDistrict_Ins" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                 
                  <label class="col-sm-2 col-form-label">รหัสไปรษณีย์</label>
                  <div class="col-sm-4">
                    <asp:TextBox id="txtPostcode_Ins" class="form-control" runat="server" onkeypress="return validatenumerics(event);" MaxLength ="5"></asp:TextBox>
                      <asp:Label ID="lblPostCode_Ins" CssClass="validatecolor"  runat="server" Width="80%"></asp:Label>
                  </div>
                  <label class="col-sm-5 col-form-label"></label>
                  <div class="text-center m-t-20">
                     <asp:Button ID="btnSubmit" Text="บันทึก" OnClick="btnSubmit_Click" 
                     class="btn-mk-pri btn-submit m-r-10"
                    runat="server" />
                    <asp:Button ID="btnCancel" Text="ล้าง" OnClick="btnCancel_Click"
                    class="btn-mk-pri btn-cancel"
                    runat="server" />
                  </div>
                </div>
                </div>
            </div>
            <!-- insert address section end -->

                    <div class="form-group row" id="SaveAddrSection" runat="server">
                        <div class="text-center m-t-20 col-sm-12" >
                            <asp:Button ID="btnSave" Text="Submit" OnClick="btnSave_Click"
                                class="btn-mk-pri btn-submit"
                                runat="server" />
                            <asp:Button ID="btnCancelSave" Text="Cancel" OnClick="btnCancelSave_Click"
                                class="btn-mk-pri btn-cancel"
                                runat="server" />

                        </div>
                    </div>
                </ContentTemplate>
        </asp:UpdatePanel>
          </div>
          </div>
        </div>
       </div>
      </div>
    
  <script type="text/javascript">
      function validatenumerics(key) {
          //getting key code of pressed key
          var keycode = (key.which) ? key.which : key.keyCode;
          //comparing pressed keycodes

          if (keycode > 31 && (keycode < 48 || keycode > 57)) {
              alert(" กรุณาระบุตัวเลข ");
              return false;
          }
          else return true;
      }

    </script>
          

  <!-- Large modal end -->
  <!-- edit address modal end -->
    



        