import React, { useEffect, useState } from 'react';
import { CircularProgress, Typography, Box, Card, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, Button } from '@mui/material';
import Menupu from './menu';

export const PropertyUnits = ({ property, onUnitSelect, allUnits, onBackToPropUnit }) => {
    const [units, setPropertyUnits] = useState(allUnits.filter(unit => unit.propertyId === property.id));
    const [showUnits, setShowUnits] = useState(false);

    useEffect(() => {
        console.log(units);
        setShowUnits(true);
          }, []);

    const handleClick = (event) => {
        onBackToPropUnit(0);
    };
    const [view, setView] = useState(true); // סטייט לניהול התצוגה
    const handleDecisionMade = (decision) => {
        setView(decision);
    };
    return (
        < >
            {showUnits ? (
                <>
                    <Menupu
                        onDecisionMade={handleDecisionMade}
                        onBackToPropUnit={handleClick}
                    />
                    {view === true && <>
                        <Card sx={{
                            width: '90%', // שינוי מגודל מקסימלי לרוחב מלא
                            maxWidth: '100%', // הסרת הגבלת הרוחב המקסימלי
                            margin: 'auto',
                            marginTop: 5,
                            padding: 2,
                            boxShadow: '0 4px 8px 0 rgba(0,0,0,0.2)', // הוספת צל לכרטיס
                            transition: '0.3s',
                            '&:hover': {
                                boxShadow: '0 8px 16px 0 rgba(0,0,0,0.2)', // הוספת אפקט צל כאשר מרחפים מעל הכרטיס
                            },
                            borderRadius: '10px', // קירות מעוגלים
                            backgroundColor: '#fff', // צבע רקע
                        }}>
                            <Typography variant="h6" component="h2" sx={{
                                marginBottom: 2,
                                fontWeight: 'bold',
                                textAlign: 'center',
                                color: '#333', // שינוי צבע הטקסט
                                marginTop: 2, // הוספת ריווח מלמעלה
                            }}>
                                Property Details
                            </Typography>
                            <Typography variant="body1" component="p" sx={{
                                textAlign: 'center',
                            }}>Address: {property.address}</Typography>
                            <Typography variant="body1" component="p" sx={{
                                textAlign: 'center',
                            }}>City: {property.city}</Typography>
                            <Typography variant="body1" component="p" sx={{
                                textAlign: 'center',
                                marginBottom: 2, // הוספת ריווח למטה
                            }}>Number of Units: {property.unitNumber}</Typography>
                        </Card>
                        <Typography variant="h6" component="h2" sx={{
                            marginBottom: 2,
                            fontWeight: 'bold',
                            textAlign: 'center',
                            color: '#333', // שינוי צבע הטקסט
                            marginTop: 2, // הוספת ריווח מלמעלה
                        }}>
                            Property Units :
                        </Typography>
                        <TableContainer component={Paper} style={{ margin: '20px', width: 'auto', overflowX: 'auto' }}>
                            <Table sx={{ minWidth: 650 }} aria-label="units table">
                                <TableHead sx={{backgroundColor: '#f2c281', '& .MuiTableCell-head': { fontSize: '1.25rem', color: 'black' }}}>
                                    <TableRow>
                                        <TableCell>Name</TableCell>
                                        <TableCell>Unit type</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {units.map((unit, index) => (
                                        <TableRow
                                            key={unit.id}
                                            onClick={() => onUnitSelect(unit, units)}
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
                                                {unit.name}
                                            </TableCell>
                                            <TableCell component="th" scope="row" sx={{ fontSize: '1.20rem' }}>
                                                {unit.type}
                                            </TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </>}
                </>
            ) : (
                <Box display="flex" justifyContent="center" alignItems="center" style={{ marginTop: '100px' }}>
                    <CircularProgress color="secondary" />
                </Box>
            )}
        </>
    );
};

export default PropertyUnits;