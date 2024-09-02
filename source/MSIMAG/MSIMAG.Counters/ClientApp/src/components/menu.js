import React, { useState } from 'react';
import { Typography, Box, Button, Card, Drawer, List, ListItem, ListItemIcon, ListItemText, Menu, MenuItem } from '@mui/material';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import InfoIcon from '@mui/icons-material/Info';
import AddIcon from '@mui/icons-material/Add';
import MenuIcon from '@mui/icons-material/Menu';
import AddMeasure from './addMeasure';
import History from './history';
import HistoryIcon from '@mui/icons-material/History';

function Menupu({ onDecisionMade,unit, onBackToPropUnit }) {
  const [anchorEl, setAnchorEl] = useState(null);
  const open = Boolean(anchorEl);

  const handleClick = (event) => {
	setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
	setAnchorEl(null);
  };

  const [view, setView] = useState('info'); // סטייט לניהול התצוגה

  return (
	<Box>
	  <Button
		aria-controls="simple-menu"
		aria-haspopup="true"
		onClick={handleClick}
		style={{ fontSize: '20px' }}
	  >
		<MenuIcon /> Menu
	  </Button>
	  <Menu
		id="simple-menu"
		anchorEl={anchorEl}
		keepMounted
		open={open}
		onClose={handleClose}
	  >
		<MenuItem onClick={() => {onBackToPropUnit(); onDecisionMade(false); handleClose(); }}><ArrowBackIcon/> BACK</MenuItem>
        <MenuItem onClick={() => { onDecisionMade(true); setView('info'); handleClose(); }}><InfoIcon/> INFO</MenuItem>
		<MenuItem onClick={() => { onDecisionMade(false);setView('addMeasure'); handleClose(); }}>
		  <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
			<AddIcon />
			ADD MEASURE
		  </div>
		</MenuItem>
		<MenuItem onClick={() => { onDecisionMade(false);setView('history'); handleClose(); }}>
		  <div style={{ display: 'flex', alignItems: 'center', gap: '8px' }}>
          <HistoryIcon />
          MEASURE HISTORY
		  </div>
		</MenuItem>
	  </Menu>
	  {view === 'addMeasure' && <AddMeasure unit={unit}/>}
      {view === 'history' && <History unit={unit}/>}
	</Box>
  );
}

export default Menupu;