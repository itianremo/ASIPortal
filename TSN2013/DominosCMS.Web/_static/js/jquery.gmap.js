(function($){
    var map;
    $.fn.gMap=function(options){
        var opts=$.extend({},$.fn.gMap.defaults,options);
        return this.each(function(){
            $(this).css('width',opts.width+'px');
            $(this).css('height',opts.height+'px');
            var geocoder=new google.maps.Geocoder();
            var centerLatLng=new google.maps.LatLng(opts.dLat,opts.dLng);
            var mapOptions={
                zoom:opts.zoom,
                center:centerLatLng,
                mapTypeId:google.maps.MapTypeId.ROADMAP
                };

            map=new google.maps.Map(this,mapOptions);
            var add_content='Your Address';
            var marker;
            var infowindow;
            if(geocoder){
                geocoder.geocode({
                    'latLng':centerLatLng
                },function(results,status){
                    if(status==google.maps.GeocoderStatus.OK){
                        add_content=results[0].formatted_address;
                        marker=new google.maps.Marker({
                            position:centerLatLng,
                            map:map,
                            title:add_content
                        });
                        infowindow=new google.maps.InfoWindow({
                            content:add_content
                        });
                        google.maps.event.addListener(marker,'click',function(){
                            infowindow.open(map,marker);
                        });
                    }
                })
            }
        if(opts.geocoding){
            google.maps.event.addListener(map,'click',function(event){
                var clickedPoint=event.latLng;
                opts.latitude.val(clickedPoint.lat());
                opts.longitude.val(clickedPoint.lng());
                var lat=opts.latitude.val();
                var lng=opts.longitude.val();
                var gecodeLatlng=new google.maps.LatLng(lat,lng);
                marker.setPosition(gecodeLatlng);
                if(geocoder){
                    geocoder.geocode({
                        'latLng':gecodeLatlng
                    },function(results,status){
                        if(status==google.maps.GeocoderStatus.OK){
                            var address=results[0].address_components;
                            var formatted_address=results[0].formatted_address;
                            marker.setTitle(formatted_address);
                            infowindow.setContent(formatted_address);
                            var address_comp=formatted_address.split(',');
                            var length=address_comp.length;
                            if(opts.city!=''){
                                opts.city.val('');
                                var city=address_comp[length-2];
                                city=city.replace(' ','')
                                opts.city.val(city);
                            }
                            if(opts.area!=''){
                                opts.area.val('');
                                var area=address_comp[length-3];
                                area=area.replace(' ','')
                                opts.area.val(area);
                            }
                            if(opts.street!=''){
                                opts.street.val('');
                                var street=address_comp[0];
                                for(var i=1;i<length-3;i++){
                                    street=street+' , '+address_comp[i];
                                }
                                opts.street.val(street);
                            }
                            if(opts.postal_code!=''){
                                opts.postal_code.val('');
                            }
                            jQuery.each(address,function(key,value){
                                switch(value.types[0]){
                                    case 'country':
                                        if(opts.country!=''&&opts.country!='*'){
                                        opts.country.val(value.long_name);
                                    }else if(opts.country=='*'){
                                        $('li[c_name="'+value.long_name+'"]').trigger('click');
                                        $('.frm_select_arrow').blur();
                                    }
                                    break;
                                    case 'postal_code':
                                        if(opts.postal_code!=''){
                                        opts.postal_code.val(value.long_name);
                                    }
                                    opts.city.val(opts.city.val().replace(' '+value.long_name,''));
                                        break;
                                }
                            });
                    }
                    });
            }
        });
}
});
};

$.fn.gMap.defaults={
    dLat:52.471462,
    dLng:-1.890196,
    zoom:12,
    width:270,
    height:270,
    geocoding:true,
    country:'',
    city:'',
    area:'',
    street:'',
    postal_code:'',
    latitude:'',
    longitude:''
};

$.fn.gMap.Load=function(){
    google.maps.event.trigger(map,'resize')
    }
})(jQuery);
