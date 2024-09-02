import React, { FunctionComponent } from 'react';
import { APIProvider, Map, Marker, Pin, AdvancedMarker } from '@vis.gl/react-google-maps';
// import {AdvancedMarker} from './advanced-marker';
const GoogleMapComponent = ({ properties }) => (
    <APIProvider apiKey={'AIzaSyC3j6_umb5mOQek3BpGUSBiorLwQwezkXo'}>
        <Map zoom={12} center={{ lat: 53.54992, lng: 10.00678 }} mapId={"3e431ea9cd204bed"}>
            {/* <AdvancedMarker position={{ lat: 53.54992, lng: 10.00678 }}>
                <Pin background={'#FBBC04'} glyphColor={'#000'} borderColor={'#000'} />
            </AdvancedMarker>    */}
             </Map>
    </APIProvider>
);
export default GoogleMapComponent;