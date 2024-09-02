import React, { useState, useEffect } from 'react';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Typography } from '@mui/material';

const History = ({ unit }) => {
    const [measurements, setMeasurements] = useState([]);

    useEffect(() => {
        if (!unit) {
            return;
        }
        const url = `https://localhost:7259/api/measure/unit/${unit.id}`;
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

                console.log(data);
                setMeasurements(data);
            })
            .catch((error) => {
                console.error("Error fetching properties:", error);
            });

    }, []);

    return (
        <>
            {unit &&
                <>
                    <Typography variant="h5" component="h4" style={{ margin: '20px' }}>
                        Measure History for unit: {unit.name}
                    </Typography>
                    <TableContainer component={Paper} style={{ margin: '20px', width: 'auto', overflowX: 'auto' }}>

                        <Table sx={{ minWidth: 650 }} aria-label="units table">
                            <TableHead sx={{ backgroundColor: '#f2c281', '& .MuiTableCell-head': { fontSize: '1.25rem', color: 'black' } }}>
                                <TableRow>
                                    <TableCell>Counter Type</TableCell>
                                    <TableCell>Meter Reading</TableCell>
                                    <TableCell>Date</TableCell>

                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {measurements.map((row, index) => (
                                    <TableRow
                                        key={index}
                                        // onClick={() => onUnitSelect(unit, units)}
                                        sx={{
                                            backgroundColor: index % 2 === 0 ? '#f5f5f5' : 'white', // צבע אפור לשורות זוגיות, לבן לשורות אי-זוגיות
                                            '&:hover': {
                                                backgroundColor: '#bacdde', // אפשרות לשינוי צבע בעת הרחפה
                                            },
                                            cursor: 'pointer', // הופך את הסמן לסמן קליק בעת הרחפה מעל שורה
                                            fontSize: '1.20rem'
                                        }}
                                    >
                                        <TableCell component="th" scope="row" sx={{ fontSize: '1.20rem' }}>
                                            {row.counterType}
                                        </TableCell>
                                        <TableCell component="th" scope="row" sx={{ fontSize: '1.20rem' }}>
                                            {row.meterReading}
                                        </TableCell>
                                        <TableCell component="th" scope="row" sx={{ fontSize: '1.20rem' }}>{new Date(row.date).toLocaleDateString()}</TableCell>

                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </>
            }
        </>
    );
};

export default History;