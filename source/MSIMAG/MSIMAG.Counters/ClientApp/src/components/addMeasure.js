import React, { useState, useEffect } from 'react';
import Checkbox from '@mui/material/Checkbox';
import FormControlLabel from '@mui/material/FormControlLabel';

import { Button, TextField, Box, Card, CardContent, CardActions, Grid } from '@mui/material';
import SwapHorizIcon from '@mui/icons-material/SwapHoriz';
import CustomSelect from './costumSelect';
import { FormControl, InputLabel, Select, MenuItem, FormHelperText } from '@mui/material';

function AddMeasure({ unit }) {
    const [file, setFile] = useState(null);
    const [previewUrl, setPreviewUrl] = useState(null); // שלב 1

    const [rentalAggrement, setRentalAggrement] = useState([]);
    const [selectedRentalAggrement, setselectedRentalAggrement] = useState([]);

    const [counter, setCounter] = useState('');
    const [measure, setMeasure] = useState('');

    const [listProtocols, setProtocols] = useState([]);
    const [listTypes, setTypes] = useState([]);
    const [listPosition, setPosition] = useState([]);

    const [selectedPosition, setSelectedPosition] = useState('');
    const [selectedProtocol, setSelectedProtocol] = useState('');
    const [selectedType, setSelectedType] = useState('');

    const [isPrimary, setIsPrimary] = useState('');

    const [belongsToTenant, setBelongsToTenant] = useState(false);

    const handleBelongsToTenantChange = (event) => {
        setBelongsToTenant(event.target.checked);
        if (event.target.checked === true) {
            getRentalAggrement();
        }
        else {
            setselectedRentalAggrement([]);
        }
    };

    useEffect(() => {
        const typeurl = `https://localhost:7259/api/measure/type`;
        const typerequestOptions = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        };

        fetch(typeurl, typerequestOptions)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then((data) => {
                setTypes(data);
            })
            .catch((error) => {
                console.error("Error fetching properties:", error);
            });

        const protocolurl = `https://localhost:7259/api/measure/protocol`;
        const protocolrequestOptions = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        };

        fetch(protocolurl, protocolrequestOptions)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then((data) => {
                setProtocols(data);
            })
            .catch((error) => {
                console.error("Error fetching properties:", error);
            });
        const positionurl = `https://localhost:7259/api/measure/position`;
        const positionrequestOptions = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        };

        fetch(positionurl, positionrequestOptions)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then((data) => {
                setPosition(data);
            })
            .catch((error) => {
                console.error("Error fetching properties:", error);
            });

    }, []);

    const handleSwap = () => {
        const temp = counter;
        setCounter(measure);
        setMeasure(temp);
    };

    const handlePositionChange = (event) => {
        setSelectedPosition(event.target.value);
    };
    const handleProtocolChange = (event) => {
        setSelectedProtocol(event.target.value);
    };
    const handleTypeChange = (event) => {
        setSelectedType(event.target.value);
    };
    const handleRentalAggrementChange = (event) => {
        setselectedRentalAggrement(event.target.value);
    };
    const handleFileChange = (event) => {
        setFile(event.target.files[0]);
        const fileReader = new FileReader();
        fileReader.onload = () => {
            setPreviewUrl(fileReader.result); // שלב 2
        };
        fileReader.readAsDataURL(event.target.files[0]);
        gendfile();

    };

    const handleSave = () => {
        if (!file || !selectedPosition || !selectedProtocol || !selectedType || !measure || !counter || !isPrimary) {
            alert('Please fill in all required fields.');
            return; // Stop the function if validation fails
        }

        const formData = new FormData();
        formData.append('FileUpload', file);
        formData.append('Position', selectedPosition);
        formData.append('Protocol', selectedProtocol);
        formData.append('CounterType', selectedType);
        if (unit) { formData.append('Unit', unit.id); }
        formData.append('MeterReading', measure);
        formData.append('MeterNumber', counter);
        formData.append('IsMainCounter', isPrimary);
        if (selectedRentalAggrement.length !== 0) {
            formData.append('RentalAgreement', selectedRentalAggrement);
        }
        else {
            formData.append('RentalAgreement', null);
        }

        const url = `https://localhost:7259/api/measure`;
        const requestOptions = {
            method: 'POST',
            body: formData,
        };

        fetch(url, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.text();
            })
            .then((data) => {
                resetForm();
                // setRentalAggrement(data)
            })
            .catch((error) => {
                console.error("Error fetching measure:", error);
            });
    };
    function resetForm() {
        setFile(null);
        setPreviewUrl(null);
        setRentalAggrement([]);
        setselectedRentalAggrement([]);
        setCounter('');
        setMeasure('');
        setProtocols([]);
        setTypes([]);
        setPosition([]);
        setSelectedPosition('');
        setSelectedProtocol('');
        setSelectedType('');
        setIsPrimary('');
        setBelongsToTenant(false);
    };

    function gendfile() {
        const formData = new FormData();
        formData.append('FileUpload', file);
        const url = `https://hook.eu1.make.com/0nc8cmgw58yy71h6ay5dp3uhvcvnam9y`;
        const requestOptions = {
            method: 'POST',
            body: formData,
        };

        fetch(url, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.text();
            })
            .then((data) => {
                console.log(data);
            })
            .catch((error) => {
                console.error("Error fetching measure:", error);
            });

    }

    function getRentalAggrement() {
        const url = `https://localhost:7259/api/data/rentalAgreements/unit/${unit.id}`;
        const requestOptions = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        };

        fetch(url, requestOptions)
            .then((response) => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then((data) => {

                setRentalAggrement(data);
            })
            .catch((error) => {
                console.error("Error fetching properties:", error);
            });
    }

    return (

        <Box display="flex" justifyContent="center" alignItems="center" minHeight="60vh">
            <Grid container justifyContent="center">
                <Grid item xs={14} sm={12} md={10} lg={8}>

                    <Card sx={{
                        width: '100%', overflow: 'visible', maxWidth: { xs: 400, sm: 600, md: 800, lg: 1000 }, mx: 'auto',
                        minHeight: '600px'
                    }}>
                        <CardContent>
                            <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', gap: 2, mb: 2 }}>
                                {unit && (
                                    <>
                                        <FormControlLabel
                                            control={<Checkbox checked={belongsToTenant} onChange={handleBelongsToTenantChange} />}
                                            label="Belongs to Tenant"
                                            sx={{ '& .MuiFormControlLabel-label': { fontSize: '1.25rem' } }}
                                        />
                                        {belongsToTenant &&
                                            <FormControl sx={{ m: 1, minWidth: 120 }}>

                                                <Select
                                                    labelId={`RentalAggrement-label`}
                                                    id={`RentalAggrement-select`}
                                                    value={selectedRentalAggrement}
                                                    onChange={handleRentalAggrementChange}
                                                    displayEmpty
                                                    inputProps={{ 'aria-label': 'Without label' }}
                                                >
                                                    <MenuItem value="" disabled>
                                                        Select a Rental Aggrement
                                                    </MenuItem>

                                                    {rentalAggrement.map((option) => (
                                                        <MenuItem key={option.id} value={option.id}>{option.rentalOffersName}</MenuItem>
                                                    ))}
                                                </Select>
                                            </FormControl>
                                        }
                                    </>
                                )}
                            </Box>
                            <Box sx={{ display: 'flex', flexDirection: { xs: 'column', sm: 'row' }, justifyContent: 'center', alignItems: 'center', gap: 2, mb: 2 }}>
                                <CustomSelect
                                    selectedValue={selectedPosition}
                                    label="Position"
                                    handleChange={handlePositionChange}
                                    options={listPosition}
                                />
                                <CustomSelect
                                    selectedValue={selectedProtocol}
                                    label="Protocol"
                                    handleChange={handleProtocolChange}
                                    options={listProtocols}
                                />
                                <CustomSelect
                                    selectedValue={selectedType}
                                    label="Type"
                                    handleChange={handleTypeChange}
                                    options={listTypes}
                                />
                                <CustomSelect
                                    selectedValue={isPrimary}
                                    label="Is Main Counter"
                                    handleChange={(e) => setIsPrimary(e.target.value)}
                                    options={[{ name: 'Ja', value: '1' }, { name: 'NEIN', value: '2' }]}
                                />
                            </Box>
                            <Box display="flex" flexDirection="column" alignItems="center" justifyContent="center" gap={2} mb={2}>
                                <Button variant="outlined" component="label" sx={{ fontSize: '1.25rem', mb: 2 }}>
                                    ADD FILE
                                    {/* <input type="file" onChange={handleFileChange} hidden /> */}
                                    <input type="file" accept="image/*" capture="camera" hidden onChange={handleFileChange} />

                                </Button>

                                <TextField
                                    label="Counter"
                                    variant="outlined"
                                    margin="normal"
                                    fullWidth
                                    value={counter}
                                    onChange={(e) => setCounter(e.target.value)}
                                    sx={{ '& .MuiInputBase-input': { fontSize: '1.25rem', backgroundColor: "WHITE" }, mb: 2 }}
                                />
                                <Button onClick={handleSwap} variant="contained" sx={{ mb: 2 }}>
                                    <SwapHorizIcon /> Swap
                                </Button>                                <TextField
                                    label="Measure"
                                    variant="outlined"
                                    margin="normal"
                                    fullWidth
                                    value={measure}
                                    onChange={(e) => setMeasure(e.target.value)}
                                    sx={{ '& .MuiInputBase-input': { fontSize: '1.25rem' }, mb: 2 }}
                                />
                            </Box>
                        </CardContent>

                        <CardActions style={{ justifyContent: 'center', gap: '20px' }}>
                            <Button onClick={handleSave} variant="outlined" component="label" sx={{ fontSize: '1.25rem', mb: 2 }}>Save</Button>
                            <Button variant="outlined" component="label" sx={{ fontSize: '1.25rem', mb: 2 }}>Save & Add Another</Button>
                        </CardActions>
                    </Card>
                </Grid>
                {previewUrl && (<Grid item xs={12} sm={5} md={4} lg={3}>
                    <Card sx={{ width: '100%', overflow: 'visible', maxWidth: { xs: 350, sm: 500, md: 700, lg: 800 }, mx: 'auto' }}>
                        <CardContent>
                            <div style={{ display: 'flex', flexDirection: 'row-reverse', alignItems: 'center' }}>

                                <img src={previewUrl} alt="Preview" style={{ maxWidth: '100%', marginLeft: '20px' }} />

                            </div>
                        </CardContent>
                    </Card>

                </Grid>
                )}
            </Grid>
        </Box>
    );
}
export default AddMeasure;