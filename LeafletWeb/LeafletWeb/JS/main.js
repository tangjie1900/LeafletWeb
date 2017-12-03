var m_map;              //添加map对象m_map
var new_area_str;
var old_ViewAreaModel;
var new_ViewAreaModel;

function load() {

    name = $("#divhaha").attr("name");
    $("#divhaha").attr("name","sadwsadw");
    //加载底图
    m_map = L.map('map');
    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
        minZoom: 3
    }).addTo(m_map);



    //L.tileLayer.wms("http://mesonet.agron.iastate.edu/cgi-bin/wms/nexrad/n0r.cgi", {
    //    layers: 'nexrad-n0r-900913',
    //    format: 'image/png',
    //    transparent: true,
    //    attribution: "Weather data © 2012 IEM Nexrad"
    //}).addTo(m_map);




    m_map.setView([31.0, 121.7], 5);
    //m_map.setMaxBounds(new L.latLngBounds(new L.latLng(30, 121), new L.latLng(31, 122)));
    //m_map.on('mousemove', GetMouseLonLat);
    m_map.on('zoomend', GetElementInArea);
    SetOldView();
}

//获取系统提交的URL的格式
function getUrl(provider, method) {
    return "PatrolHandler.do?provider=" + provider + "&method=" + method;
}

GetMouseLonLat = function (e) {
    var _lat = e.latlng.lat;
    var _lng = e.latlng.lng;
    var zoomlvl = m_map.getZoom();
    $("#lng_div,#lat_div,#zoom_div").css("display", "block");
    $("#lngval_div").html(GetLatLngInFormat(_lng));
    $("#latval_div").html(GetLatLngInFormat(_lat));
    $("#zoomval_div").html(zoomlvl + "级");

    GetMapBounds();
    $("#view_area").html(GetElementInArea);
}


GetLatLngInFormat = function (val) {
    var du = parseInt(val);
    var _fen = (val - du) * 60;
    var fen = parseInt(_fen);
    var _miao = (_fen - fen) * 60;
    var miao = Math.round(_miao, 2);
    return du + "°" + fen + "'" + miao + "\"";
}

SetOldView = function () {
    GetMapBounds();
    if (old_ViewAreaModel == undefined || old_ViewAreaModel == null) {
        old_ViewAreaModel = new ViewAreaModel();
        old_ViewAreaModel.minlon = new_ViewAreaModel.minlon;
        old_ViewAreaModel.minlat = new_ViewAreaModel.minlat;
        old_ViewAreaModel.maxlon = new_ViewAreaModel.maxlon;
        old_ViewAreaModel.maxlat = new_ViewAreaModel.maxlat;
        old_ViewAreaModel.zoom = new_ViewAreaModel.zoom;
    }
}

GetMapBounds = function () {
    var bounds = m_map.getBounds();
    var left_up = bounds.getNorthWest();
    var right_up = bounds.getNorthEast();

    var left_down = bounds.getSouthWest();
    var right_down = bounds.getSouthEast();
    var viwe_area_value = GetLatLngInFormat(left_up.lat) + " " + GetLatLngInFormat(left_up.lng) + " " + GetLatLngInFormat(right_down.lat) + " " + GetLatLngInFormat(right_down.lng);

    if (new_ViewAreaModel == undefined || new_ViewAreaModel == null) {
        new_ViewAreaModel = new ViewAreaModel();
    }
    new_ViewAreaModel.minlon = left_up.lng;
    new_ViewAreaModel.minlat = right_down.lat;
    new_ViewAreaModel.maxlon = right_down.lng;
    new_ViewAreaModel.maxlat = left_up.lat;
    new_ViewAreaModel.zoom = m_map.getZoom();
}


GetElementInArea = function () {
    SetOldView();
    if (IsNeedRedraw()) {
        new_area_str = new_ViewAreaModel.minlon + ',' + new_ViewAreaModel.minlat + ',' + new_ViewAreaModel.maxlon + ',' + new_ViewAreaModel.maxlat;
        console.log(new_area_str);
        $.ajax(
           {
               url: getUrl('NcConvert.Program', 'Excute'),
               type: 'post',
               dataType: 'json',
               data: { area: new_area_str, ncfilepath: "" },
               success: function (data) {
                   console.log(data);
               }
           })
    }
}


//判断是否需要重新绘制
IsNeedRedraw = function () {
    if (old_ViewAreaModel == undefined || old_ViewAreaModel == null || new_ViewAreaModel == undefined || new_ViewAreaModel == null) {
        return false;
    }
    else if (old_ViewAreaModel.zoom != new_ViewAreaModel.zoom) {
        return true;
    }
    else {
        return false;
    }
}


function ViewAreaModel() {
    this.minlon = 0;
    this.minlat = 0;
    this.maxlon = 0;
    this.maxlat = 0;
    this.zoom = 0;
}

