import React, { useEffect, useState } from 'react';
import Loading from './loading';
import Box from '@mui/material/Box';
import logo from './css/logo.png';
import PropertyUnits from './propertyUnits';
import UnitDetails from './unitDeatails';
import { Tabs, Tab, Breadcrumbs, Link, Typography } from '@mui/material';
import HomePage from './homePage';

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
                <Box p={3}>
                    {children}
                </Box>
            )
            }
        </div>
    );
}

const Home = () => {

    const [showProperties, setShowProperties] = useState(false);
    const [properties, setProperties] = useState([]);
    const [selectedPropertyId, setSelectedPropertyId] = useState(null);
    const [selectedUnitId, setSelectedUnitId] = useState(null);
    const [propUnits, setPropUnits] = useState([]);
    const [allUnits, setUnits] = useState([]);
    const [value, setValue] = useState(0);

    const handleUnitSelect = (unitId, units) => {
        setSelectedUnitId(unitId);
        setPropUnits(units);
    };

    const handelBack = (flag) => {
        setValue(flag);
    }

    useEffect(() => {
        const url = "https://localhost:7259/api/data/properties";
        fetch(url, {
            method: "GET",
            headers: { "Content-Type": "application/json" },
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                setProperties(data);
                const cities = data.map(building => building.city);

                // שימוש ב-Set להסרת כפילויות והמרתו חזרה למערך
                const uniqueCities = [...new Set(cities)];
                const cityCoordinates = data.reduce((acc, building) => {
                    if (!acc[building.city] && building.lat !== 0 && building.lng !== 0) {
                        acc[building.city] = { lat: building.lat, lng: building.lng };
                    }
                    return acc;
                }, {});

                const uniqueCitiesWithCoordinates = Object.keys(cityCoordinates).map(city => ({
                    city,
                    lat: cityCoordinates[city].lat,
                    lng: cityCoordinates[city].lng
                }));

            })
            .catch(error => {
                console.error("Error fetching properties:", error);
            });

        const url2 = `https://localhost:7259/api/data/units`;
        const requestOptions = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        };

        fetch(url2, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then((data) => {
                setUnits(data);
                // setShowUnits(true);
                setShowProperties(true);
                console.log(data);
            })
            .catch((error) => {
                console.error("Error fetching properties:", error);
            });
    }, []);

    const handleChange = (event, newValue, forcedValue = null) => {
        setValue(forcedValue !== null ? forcedValue : newValue);
    };

    useEffect(() => {
        if (selectedPropertyId) {
            handleChange(null, null, 1);
        }
    }, [selectedPropertyId]);

    useEffect(() => {
        if (selectedUnitId) {
            handleChange(null, null, 2); // Here, nulls are placeholders since we're forcing the value
        }
    }, [selectedUnitId]); // This effect depends on selectedPropertyId

    const handlePropertySelect = (propertyId) => {
        setPropUnits([]);
        setSelectedPropertyId(propertyId);
        setSelectedUnitId(null);
    };

    return (

        <div style={{ width: '100vw', height: '100vh', backgroundColor: '#f5f5f5' }}>
            {showProperties ? (
                <div>
                    <div style={{
                        backgroundColor: '#fff',
                        boxShadow: '0 2px 4px rgba(0,0,0,0.1)',
                        position: 'fixed',
                        top: 0,
                        width: '100%',
                        zIndex: 1000,
                        display: 'flex',
                        flexDirection: 'column', // Change display to column to stack elements vertically
                    }}>
                        <div style={{ display: 'flex', alignItems: 'center', zIndex: 1001, paddingLeft: '10px' }}>
                            <img src={logo} alt="Logo" style={{ height: '50px', marginRight: 'auto' }} />
                        </div>

                        <Breadcrumbs aria-label="breadcrumb" style={{ color: "white", display: 'flex', alignItems: 'center', padding: '10px 0', backgroundColor: '#688299' }}>
                            <Link
                                color="inherit"
                                href="#"
                                onClick={() => setValue(0)}
                                style={value === 0 ? { fontWeight: 'bold', fontSize: '22px',paddingLeft: '10px' } : { fontSize: '20px', paddingLeft: '10px' }}
                                onMouseOver={(e) => (e.target.style.textDecoration = 'underline')}
                                onMouseOut={(e) => (e.target.style.textDecoration = 'none')}
                            >
                                Home / Properties
                            </Link>

                            {selectedPropertyId && (
                                <Link
                                    color="inherit"
                                    href="#"
                                    onClick={() => setValue(1)}
                                    style={value === 1 ? { fontWeight: 'bold', fontSize: '22px' } : { fontSize: '20px' }}
                                    onMouseOver={(e) => (e.target.style.textDecoration = 'underline')}
                                    onMouseOut={(e) => (e.target.style.textDecoration = 'none')}
                                >
                                    {selectedPropertyId.address}
                                </Link>
                            )}
                            {selectedUnitId && (
                                <Link
                                    color="inherit"
                                    href="#"
                                    onClick={() => setValue(2)}
                                    style={value === 2 ? { fontWeight: 'bold', fontSize: '22px' } : { fontSize: '20px' }}
                                    onMouseOver={(e) => (e.target.style.textDecoration = 'underline')}
                                    onMouseOut={(e) => (e.target.style.textDecoration = 'none')}
                                >
                                    Unit / {selectedUnitId.name}
                                </Link>
                            )}
                        </Breadcrumbs>
                    </div>
                    <div >

                        {value === 0 && (

                            <><TabPanel value={value} index={0} >
                                <HomePage properties={properties} onPropertySelect1={handlePropertySelect} />
                            </TabPanel>
                            </>
                        )}
                        {selectedPropertyId &&
                            <>
                                <TabPanel value={value} index={1} style={{ paddingTop: '70px' }}>
                                    <PropertyUnits property={selectedPropertyId} onUnitSelect={handleUnitSelect} allUnits={allUnits} onBackToPropUnit={handelBack} />
                                </TabPanel>

                                {selectedUnitId &&
                                    <TabPanel value={value} index={2} style={{ paddingTop: '70px' }}>
                                        <UnitDetails unit={selectedUnitId} onBackToPropUnit={handelBack} />
                                    </TabPanel>
                                }
                            </>
                        }

                    </div>
                </div>) : <Loading />}
        </div>
    );
};
export default Home;