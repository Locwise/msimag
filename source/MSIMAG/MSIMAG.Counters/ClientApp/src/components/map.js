import React, { useState, useEffect } from 'react';
import { MapContainer, TileLayer, Marker, Popup, useMap } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import L from 'leaflet';
import icon from './icon.png';
import './css/map.css';
import { Box, Typography, Card, MenuItem, FormControl, Select } from '@mui/material';

const customIcon = new L.Icon({
    iconUrl: icon, // Specify the path to your icon image
    iconSize: [35, 35], // Size of the icon
    iconAnchor: [17, 35], // Point of the icon which will correspond to marker's location
    popupAnchor: [0, -35], // Point from which the popup should open relative to the iconAnchor
});

// Component to update map center
function ChangeView({ center, zoom }) {
    const map = useMap();
    map.setView(center, zoom);
    return null;
}

const MapWithProperties = ({ properties, onPropertySelect,center }) => {
    const [mapProperties, setMapProperties] = useState(properties);
    const [zoom, setZoom] = useState(10);

    return (
        <div style={{ display: 'flex', justifyContent: 'center', height: '80vh' }}>

            <Card  style={{ height: '70vh', width: '90vw', overflow: 'hidden' }}>
                <MapContainer center={center} zoom={10} style={{ height: '90vh', width: '90vw' }}>

                    <TileLayer
                        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                        attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    />
                    {mapProperties.map((property) => {
                        return (
                            <Marker key={property.id} position={[property.longitude, property.latitude]} icon={customIcon}>
                                <Popup>
                                    <div className="popup-content">
                                        <Box sx={{ padding: 2, margin: 1, border: '1px solid #ccc', borderRadius: '4px', backgroundColor: '#f9f9f9' }}>
                                            <Typography variant="h6" component="h2" sx={{ marginBottom: 1 }}>
                                                Property Details:
                                            </Typography>
                                            <Typography variant="body1" component="p">
                                                Address: {property.address}
                                            </Typography>
                                            <Typography variant="body1" component="p">
                                                City: {property.city}
                                            </Typography>
                                            <Typography variant="body1" component="p">
                                                Number of Units: {property.unitNumber}
                                            </Typography>
                                        </Box>
                                        <button className="popup-button" onClick={() => onPropertySelect(property)}>Show property Units</button>
                                    </div>
                                </Popup>
                            </Marker>
                        );
                    })}
                    <ChangeView center={center} zoom={zoom} />

                </MapContainer>
            </Card>
</div>
    );
};

export default MapWithProperties;