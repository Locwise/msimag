import React from 'react';
import './css/loading.css'; // ודא שהקובץ CSS נטען כראוי
import logo from './css/logo.png'; // החלף בנתיב הנכון ללוגו שלך
import { CircularProgress} from '@mui/material';

function Loading() {
  return (
	<div className="logo-container">
	  <img src={logo} className="App-logo" alt="logo" />
	  <CircularProgress style={{ color: 'linear-gradient(to right, grey, orange)' }} />

	</div>
  );
}

export default Loading;