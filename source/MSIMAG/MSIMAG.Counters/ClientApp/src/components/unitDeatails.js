import React, { useState } from 'react';
import { Typography, Box, Button, Card, Drawer, List, ListItem, ListItemIcon, ListItemText, Menu, MenuItem } from '@mui/material';

import Menupu from './menu';

function UnitDetails({ unit, onBackToPropUnit }) {

  const handleClick = (event) => {
    onBackToPropUnit(1);
  };

  const [view, setView] = useState(true); // סטייט לניהול התצוגה
  const handleDecisionMade = (decision) => {
    setView(decision);
  };
  return (
    <>
      <Menupu
        onDecisionMade={handleDecisionMade}
        onBackToPropUnit={handleClick}
        unit={unit}
      />
      {view == true &&
        <Card sx={{
          width: '90%',
          maxWidth: '100%',
          margin: 'auto',
          marginTop: 5,
          padding: 2,
          boxShadow: '0 4px 8px 0 rgba(0,0,0,0.2)',
          transition: '0.3s',
          '&:hover': {
            boxShadow: '0 8px 16px 0 rgba(0,0,0,0.2)',
          },
          borderRadius: '10px',
          backgroundColor: '#fff',
        }}>
          <Typography sx={{
            marginBottom: 2,
            fontWeight: 'bold',
            textAlign: 'center',
            color: '#333',
            marginTop: 2,
          }} variant="h5" component="h2" gutterBottom>
            Unit : {unit.name}
          </Typography>
          {unit.type && (
            <Typography sx={{
              textAlign: 'center',
              marginBottom: 2,
            }} variant="body1">
              Type : {unit.type}
            </Typography>
          )}
        </Card>}


    </>
  );
}

export default UnitDetails;