import React, { useState } from 'react';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Button, Paper, TextField, Select, MenuItem, FormControl, InputLabel } from '@mui/material';

const ListOfProperties = ({ properties, onPropertySelect }) => {
    const [sortConfig, setSortConfig] = useState({ key: null, direction: 'ascending' });
    const [cityFilter, setCityFilter] = useState('');

    const requestSort = (key) => {
        let direction = 'ascending';
        if (sortConfig.key === key && sortConfig.direction === 'ascending') {
            direction = 'descending';
        }
        setSortConfig({ key, direction });
    };

    const sortedAndFilteredProperties = React.useMemo(() => {
        let filteredItems = properties.filter(property =>
            String(property.city).includes(cityFilter)
        );
        if (sortConfig !== null) {
            filteredItems.sort((a, b) => {
                if (a[sortConfig.key] < b[sortConfig.key]) {
                    return sortConfig.direction === 'ascending' ? -1 : 1;
                }
                if (a[sortConfig.key] > b[sortConfig.key]) {
                    return sortConfig.direction === 'ascending' ? 1 : -1;
                }
                return 0;
            });
        }
        return filteredItems;
    }, [properties, sortConfig, cityFilter]);

    return (
        <div style={{ display: 'flex', flexDirection: 'row'}}>
            <TableContainer component={Paper} style={{ margin: '20px', width: 'auto', flexGrow: 1 }}>
                <Table sx={{ minWidth: 650 }} aria-label="properties table">
                    <TableHead sx={{ backgroundColor: '#f2c281', '& .MuiTableCell-head': { fontSize: '1.25rem', color: 'black' } }}>
                        <TableRow>
                            <TableCell onClick={() => requestSort('address')}>Address</TableCell>
                            <TableCell onClick={() => requestSort('city')}>City</TableCell>
                            <TableCell onClick={() => requestSort('unitNumber')}>#Unit</TableCell>
                            {/* <TableCell></TableCell> */}
                        </TableRow>
                    </TableHead>
                    <TableBody >
                        {sortedAndFilteredProperties.map((property,index) => (
                            <TableRow
                                key={property.id}
                                // sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
                                onClick={() => onPropertySelect(property)}
                                sx={{
                                    backgroundColor: index % 2 === 0 ? '#f5f5f5' : 'white', // צבע אפור לשורות זוגיות, לבן לשורות אי-זוגיות
                                    '&:hover': {
                                        backgroundColor: '#bacdde', // אפשרות לשינוי צבע בעת הרחפה
                                    },
                                    cursor: 'pointer', // הופך את הסמן לסמן קליק בעת הרחפה מעל שורה
                                    fontSize: '1.20rem',
                                     
                                }}
                            >
                                <TableCell component="th" scope="row" sx={{ fontSize: '1.20rem' }}>
                                    {property.address}
                                </TableCell>
                                <TableCell sx={{ fontSize: '1.20rem' }}>{property.city}</TableCell>
                                <TableCell sx={{ fontSize: '1.2rem' }}>{property.unitNumber}</TableCell>

                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </div>
    );
};

export default ListOfProperties;