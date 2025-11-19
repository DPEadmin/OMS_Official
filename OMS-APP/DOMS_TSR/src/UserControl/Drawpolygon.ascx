<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Drawpolygon.ascx.cs" Inherits="DOMS_TSR.src.UserControl.Drawpolygon" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


    <style>
      /* Always set the map height explicitly to define the size of the div element that contains the map. */
      #map {
        height: 400px;
      }
 
    </style>

<!-- แสดง Modal -->
<div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true" id="modal-address" style="z-index: 1051;">
    <div class="modal-dialog modal-lg" style="max-width: 900px;">
        <div class="modal-content">
            
               
                    <div class="row">  
                    <div class="col-sm-12">
                        <div class="modal-header modal-header2  p-l-0">
                            <div class="col-sm-12">
                                <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                                <div class="modal-title sub-title " style="font-size: 16px;">Inventory Name : <asp:Label ID="LbNameInventory" runat="server"></asp:Label></div>
                            </div>
                            <span>
                                <button type="button" class="close" style="padding-left: 0px; padding-right: 0px;" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </span>
                        </div>
                    </div>
                
                    </div>
                    <div class="modal-body">
                        <div class="card-block">
                            <div class="form-group row">

                                    <label class="col-sm-2 col-form-label">Lat</label>
                                    <div class="col-sm-4">
                                        <asp:Label ID="PopmapLbLat"  runat="server"></asp:Label>
                                    </div>

                                    

                                    <label class="col-sm-2 col-form-label">Long</label>
                                    <div class="col-sm-4">
                                        <asp:Label ID="PopmapLbLong"  runat="server"></asp:Label>
                                    </div>
                                    <asp:HiddenField ID="hidInvenIDPopupMap" runat="server" />
                                  
                               
                                </div>
                        <div class="map" id="map" style="height: 400px;"></div>
                        <textarea id="coordinates" ></textarea>
                            <input type="hidden" id="coordinatesHiddenField" runat="server" />
                            <div class="text-center m-t-20 col-sm-12">
                                            <asp:LinkButton ID="PopupMapSave" class="button-action button-add" OnClick="btnPopupMapSave_Click" data-backdrop="false" runat="server">Save</asp:LinkButton>
                                            <asp:LinkButton ID="PopupMapReset" class="button-action button-delete" OnClick="btnMapReset_Click" runat="server">Reset</asp:LinkButton>
                                        </div>
                    </div>
                   </div>
             
        </div>
    </div>
</div>

<!-- ส่วนของสคริปต์ -->
<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDxXydXu4wn2OYdh3SnLkwr9mbMRC-3cxQ&callback=initMap&libraries=drawing&v=weekly" defer></script>

<script src="https://polyfill.io/v3/polyfill.min.js?features=default"></script>




<script>

    
    let polygonline = ""; 
   
let map;
let drawingManager;
    let polygon;
    let polygonz;
    let polygonCoordinates = [];
    function UpdateLabels(strlat, strLong, idinven, PolyLine) {


        document.getElementById('<%= PopmapLbLat.ClientID %>').textContent = strlat;
    document.getElementById('<%= PopmapLbLong.ClientID %>').textContent = strLong;
    document.getElementById('<%= hidInvenIDPopupMap.ClientID %>').value = idinven;

    if (PolyLine && PolyLine.trim() !== "") {
        polygonline = PolyLine;
       
        
    }




}

function initMap() {
    const myLatLng = { lat: parseFloat(strlat), lng: parseFloat(strLong) }; 
    map = new google.maps.Map(document.getElementById("map"), {
        center: myLatLng,
        zoom: 12,
    });
    new google.maps.Marker({
        position: myLatLng,
        map,
        title: "Hello World!",
    });

    drawingManager = new google.maps.drawing.DrawingManager({
        drawingMode: google.maps.drawing.OverlayType.POLYGON,
        drawingControl: true,
        drawingControlOptions: {
            position: google.maps.ControlPosition.TOP_CENTER,
            drawingModes: [google.maps.drawing.OverlayType.POLYGON],
        },
    });

    drawingManager.setMap(map);

    google.maps.event.addListener(drawingManager, "polygoncomplete", function (newPolygon) {
        polygon = newPolygon;
        google.maps.event.addListener(polygon.getPath(), "insert_at", updatePolygonCoordinates);
        google.maps.event.addListener(polygon.getPath(), "set_at", updatePolygonCoordinates);
        updatePolygonCoordinates();
    });
    let pp = polygonline;
     

    const latLngArray = pp.split(" "); // แยกข้อมูลที่แต่ละบรรทัด
    const latLngPairs = [];

    for (let i = 0; i < latLngArray.length; i++) {
        const latLng = latLngArray[i].split(","); // แยกพิกัด lat และ lng
        const lat = parseFloat(latLng[0]);
        const lng = parseFloat(latLng[1]);
        latLngPairs.push({ lat: lat, lng: lng });
    }

    const polygonz = new google.maps.Polygon({
        paths: latLngPairs, // ต้องใส่ latLngPairs ในอาร์เรย์ซ้อนกัน
        strokeColor: "#FF0000",
        strokeOpacity: 0.8,
        strokeWeight: 3,
        fillColor: "#FF0000",
        fillOpacity: 0.35,
    });
   
    polygonz.setMap(map);
   
    marker.setMap(map);
    

}

    function updatePolygonCoordinates() {
        if (!polygon) {
            return; // Exit the function if polygon is not defined yet
        }

        polygonCoordinates = [];
        const path = polygon.getPath();

        for (let i = 0; i < path.getLength(); i++) {
            const latLng = path.getAt(i);
            polygonCoordinates.push(`${latLng.lat()},${latLng.lng()}`);
        }

        const coordinatesHiddenField = document.getElementById('<%= coordinatesHiddenField.ClientID %>');
        coordinatesHiddenField.value = polygonCoordinates.join(" "); // Join the coordinates with commas

        // Build coordinates text
        let coordinatesText = polygonCoordinates.join(" "); // Join without additional comma

        // Update the 'coordinates' textarea
        const coordinatesTextArea = document.getElementById("coordinates");
        coordinatesTextArea.value = coordinatesText.replace(/(\r\n|\n|\r)/gm, ' '); // Replace newlines with spaces
    }



 
</script>  


