import './App.css';
import Home from './components/home';
import { createMuiTheme, ThemeProvider } from '@mui/material/styles';
function App() {
  const theme = createMuiTheme({
    typography: {
      fontFamily: 'Montserrat, sans-serif',
    },
  });

  return (
    <ThemeProvider theme={theme}>
      <Home />
    </ThemeProvider>
  );
}

export default App;