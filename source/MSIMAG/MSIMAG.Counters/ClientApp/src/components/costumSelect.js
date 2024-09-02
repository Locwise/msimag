import React from 'react';
import { FormControl, InputLabel, Select, MenuItem, FormHelperText } from '@mui/material';

function CustomSelect({ label, selectedValue, handleChange, options }) {
    return (
        <FormControl sx={{ m: 1, minWidth: 120 }}>
            <InputLabel id={`${label}-label`}>{label}</InputLabel>
            <Select
                labelId={`${label}-label`}
                id={`${label}-select`}
                value={selectedValue}
                label={label}
                onChange={handleChange}
            >
                {options.map((option) => (
                    <MenuItem key={option.value} value={option.value}>{option.name}</MenuItem>
                ))}
            </Select>
            <FormHelperText>Select {label.toLowerCase()}</FormHelperText>
        </FormControl>
    );
}

export default CustomSelect;