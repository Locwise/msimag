import React, { useEffect, useState } from 'react';
import MapWithProperties from './map';
import MapIcon from '@mui/icons-material/Map';
import ListIcon from '@mui/icons-material/List';
import Box from '@mui/material/Box';
import ListOfProperties from './listOfProperties';
import { Tabs, Tab, Breadcrumbs, Link, Typography } from '@mui/material';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button, Paper, TextField, Select, MenuItem, FormControl, InputLabel } from '@mui/material';
import CustomSelect from './costumSelect';

function TabPanel(props) {
    const { children, value, index, ...other } = props;
    return (
        <div
            role="tabpanel"
            hidden={value !== index}
            id={`simple-tabpanel-${index}`}
            aria-labelledby={`simple-tab-${index}`}
            {...other}
        >
            {value === index && (
                <Box p={2}>
                    {children}
                </Box>
            )}
        </div>
    );
}

const HomePage = ({ properties, onPropertySelect1 }) => {
    const [uniqueCitiesWithCoordinates, setUniqueCitiesWithCoordinates] = useState([]);
    const [selectedCity, setSelectedCity] = useState('');
    const [mapProperties, setMapProperties] = useState(properties.filter(d => d.latitude !== 0 || d.longitude !== 0));

    const [center, setCenter] = useState([52.59799828085434, 13.380454746842618]);

    const [selectedPropertyId, setSelectedPropertyId] = useState(null);
    const [selectedUnitId, setSelectedUnitId] = useState(null);

    const [allcities, setCities] = useState([]);
    const [selectedcity, setSelectedcity] = useState("select all");
    const [filteredcity, setFilteredcity] = useState([]);
    useEffect(() => {
        const url = `https://localhost:7259/api/data/cities`;
        const requestOptions = {
            method: 'GET',
        };

        fetch(url, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then((data) => {
                console.log(data);
                setCities(data);
            })
            .catch((error) => {
                console.error("Error fetching measure:", error);
            });
    }, []);

    const handlecityChange = (event) => {
        if (event.target.value === "all") {
            setFilteredcity(properties);
            setSelectedcity(event.target.value);
            return;
        }
        console.log(properties);
        setSelectedcity(event.target.value);
        const city = allcities.find(c => c.id === event.target.value);
        console.log(city);
        setCenter([city.latitude, city.longitude]);
        const filter = properties.filter(c => c.cityID === event.target.value);
        console.log(filter);
        setFilteredcity(filter);
    }

    const [cityFilter, setCityFilter] = useState('');
    const cities = properties.map(property => property.city);
    const uniqueCities = [...new Set(cities)];
    const handleCityFilterChange = (event) => {
        setSelectedCity(event.target.value);
        setCityFilter(event.target.value);
        const value = event.target.value;
        // Set cityFilter to whatever logic you use to represent "all" cities
        // This could mean setting it to an empty string, null, or a special value
        // that your filtering logic recognizes as "show all"
        if (value === "all") {
            // Adjust this to match how you handle showing all cities
            setCityFilter('');
        } else {
            setCityFilter(value);
        }
    };
    const sortedAndFilteredProperties = React.useMemo(() => {
        let filteredItems = properties.filter(property =>
            String(property.city).includes(cityFilter)
        );

        return filteredItems;
    }, [properties, cityFilter]);

    const [value, setValue] = useState(0);

    const handlePropertySelect = (propertyId) => {
        onPropertySelect1(propertyId);
        setSelectedPropertyId(propertyId);
    };

    const handleChange = (event, newValue, forcedValue = null) => {
        setValue(forcedValue !== null ? forcedValue : newValue);
    };

    useEffect(() => {
        if (selectedUnitId) {
            // Automatically switch to the " UNITS" tab
            handleChange(null, null, 3); // Here, nulls are placeholders since we're forcing the value
        }
    }, [selectedUnitId]); // This effect depends on selectedPropertyId
    const cityCoordinates = mapProperties.reduce((acc, building) => {
        if (!acc[building.city] && building.latitude !== 0 && building.longitude !== 0) {
            acc[building.city] = { latitude: building.latitude, longitude: building.longitude };
        }
        return acc;
    }, {});

    useEffect(() => {
        setUniqueCitiesWithCoordinates(Object.keys(cityCoordinates).map(city => ({
            city,
            latitude: cityCoordinates[city].latitude,
            longitude: cityCoordinates[city].longitude
        })))
    }, []);

    useEffect(() => {
        const city = uniqueCitiesWithCoordinates.find(c => c.city === selectedCity);
        if (city) {
            setCenter([city.longitude, city.latitude]);
        }
    }, [selectedCity, uniqueCitiesWithCoordinates]);
    const handleCityChange = (event) => {
        setSelectedCity(event.target.value);
        setCityFilter(event.target.value);
    };

    return (
        <div>
            <div style={{ display: 'flex', alignItems: 'center', flexWrap: 'wrap' }}>
                <Tabs value={value} onChange={handleChange} aria-label="icon label tabs example" style={{ paddingTop: '70px' }}>
                    <Tab icon={<MapIcon />} label="MAP" />
                    <Tab icon={<ListIcon />} label="LIST" />
                </Tabs>

                {/* <FormControl style={{ margin: '0 20px', width: '200px', paddingTop: '95px' }}>
                    <InputLabel>Filter by City</InputLabel>
                    {value == 0 &&
                        <Select
                            value={selectedCity}
                            onChange={handleCityChange}
                            displayEmpty
                            inputProps={{ 'aria-label': 'Without label' }}
                            sx={{ bgcolor: 'white', borderRadius: 2 }}
                        >
                            <MenuItem value="" disabled>
                                Select a City
                            </MenuItem>
                            {uniqueCitiesWithCoordinates.map((city) => (
                                <MenuItem key={city.city} value={city.city}>{city.city}</MenuItem>
                            ))}
                        </Select>}
                    {value == 1 &&
                        <>
                            <InputLabel>Filter by City</InputLabel>
                            <Select
                                value={cityFilter}
                                onChange={handleCityFilterChange}
                                displayEmpty
                                inputProps={{ 'aria-label': 'Without label' }}
                                sx={{ bgcolor: 'white', borderRadius: 2 }}

                            >
                                {cityFilter === "" ? (
                                    <MenuItem value="" disabled>
                                        No city selected
                                    </MenuItem>
                                ) : (
                                    <MenuItem value="" disabled>
                                        Select a City
                                    </MenuItem>
                                )}
                                <MenuItem value="all" key="all">
                                    Select All
                                </MenuItem>
                                {uniqueCities.map((city) => (
                                    <MenuItem key={city} value={city}>{city}</MenuItem>
                                ))}
                            </Select>
                        </>}
                </FormControl> */}

                <FormControl style={{ margin: '0 20px', width: '200px', paddingTop: '95px' }}>
                    <InputLabel>Select City</InputLabel>
                    <Select
                        labelId={`select city-label`}
                        id={`select city-select`}
                        value={selectedcity}
                        label="select city"
                        onChange={handlecityChange}
                    >
                        {value==0 && <MenuItem value="" disabled>select city</MenuItem>}
                        {value == 1 &&
                            <MenuItem value="all">
                                select all
                            </MenuItem>
                        }
                        {allcities.map((option) => (
                            <MenuItem key={option.id} value={option.id}>{option.name}</MenuItem>
                        ))}
                    </Select>
                </FormControl>
            </div>

            <TabPanel value={value} index={0}>
                <MapWithProperties properties={mapProperties} onPropertySelect={handlePropertySelect} center={center} />
            </TabPanel>
            <TabPanel value={value} index={1}>
                {/* <ListOfProperties properties={sortedAndFilteredProperties} onPropertySelect={handlePropertySelect} /> */}
                <ListOfProperties properties={filteredcity} onPropertySelect={handlePropertySelect} />

            </TabPanel>
        </div>
    );
};
export default HomePage;