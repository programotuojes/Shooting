import {BrowserRouter, Route, Routes} from 'react-router-dom';
import Login from './pages/auth/Login';
import {CssBaseline} from '@mui/material';
import {createTheme, ThemeProvider} from '@mui/material/styles';
import Posts from './pages/posts/Posts';
import Header from './components/Header';
// eslint-disable-next-line
import Footer from './components/Footer';
import EditPost from './pages/posts/EditPost';
import Post from './pages/posts/Post';
import Register from './pages/auth/Register';
import Competitions from './pages/competitions/Competitions';
import EditCompetition from './pages/competitions/EditCompetition';
import Competition from './pages/competitions/Competition';

let theme = createTheme();

theme = createTheme(theme, {
  components: {
    MuiAppBar: {
      styleOverrides: {
        root: {
          zIndex: theme.zIndex.drawer + 1,
          marginBottom: '36px'
        }
      }
    },
    MuiInputBase: {
      styleOverrides: {
        input: {
          padding: '12px'
        }
      }
    },
    MuiButton: {
      styleOverrides: {
        root: {
          textTransform: 'none'
        }
      }
    }
  }
});

function App() {
  return (
      <ThemeProvider theme={theme}>
        <CssBaseline />

        <BrowserRouter>

          <Header />

          <Routes>
            <Route path='/' element={<Posts />} />
            <Route path='/posts/new' element={<EditPost />} />
            <Route path='/posts/:postId' element={<Post />} />
            <Route path='/posts/:postId/edit' element={<EditPost />} />

            <Route path='/competitions' element={<Competitions />} />
            <Route path='/competitions/new' element={<EditCompetition />} />
            <Route path='/competitions/:competitionId' element={<Competition />} />
            <Route path='/competitions/:competitionId/edit' element={<EditCompetition />} />

            <Route path='/login' element={<Login />} />
            <Route path='/register' element={<Register />} />
          </Routes>

          <Footer />

        </BrowserRouter>
      </ThemeProvider>
  );
}

export default App;
