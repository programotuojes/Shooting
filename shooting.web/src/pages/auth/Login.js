import React, {useState} from 'react';
import Avatar from '@mui/material/Avatar';
import LoadingButton from '@mui/lab/LoadingButton';
import TextField from '@mui/material/TextField';
import {Link, useNavigate} from 'react-router-dom';
import Grid from '@mui/material/Grid';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import {STORE_ROLE, STORE_TOKEN, STORE_USERNAME, URL} from '../../Constants';
import {Alert} from '@mui/material';
import Snackbar from '@mui/material/Snackbar';

export default function Login() {
  const navigate = useNavigate();

  const [usernameError, setUsernameError] = useState(false);
  const [passwordError, setPasswordError] = useState(false);
  const [loading, setLoading] = useState(false);
  const [showToast, setShowToast] = useState(false);

  const handleSubmit = (event) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const userData = {
      username: formData.get('username'),
      password: formData.get('password'),
    };

    const userError = userData.username.length < 2 || userData.username.length
        > 20;
    const passError = userData.password.length === 0;
    setUsernameError(userError);
    setPasswordError(passError);
    if (userError || passError) {
      return;
    }

    setLoading(true);

    fetch(`${URL}/users/login`, {
      method: 'POST',
      headers: {'Content-Type': 'application/json'},
      body: JSON.stringify(userData)
    })
    .then(res => res.ok ? res.json() : Promise.reject())
    .then(data => {
      sessionStorage.setItem(STORE_USERNAME, data.username);
      sessionStorage.setItem(STORE_ROLE, data.role);
      sessionStorage.setItem(STORE_TOKEN, data.token);
      setLoading(false);
      navigate(-1);
    })
    .catch(() => {
      setLoading(false);
      setShowToast(true);
    });
  };

  return (
      <Container maxWidth={'sm'}>
        <Grid container
              alignItems={'center'}
              direction={'column'}
              spacing={3}
        >

          <Grid item>
            <Avatar sx={{bgcolor: 'secondary.main'}}>
              <LockOutlinedIcon />
            </Avatar>
          </Grid>

          <Grid item>
            <Typography component="h1" variant="h5">
              Prisijungimo langas
            </Typography>
          </Grid>

          <Grid container
                spacing={1}
                item
                direction={'column'}
                component="form"
                onSubmit={handleSubmit}
                noValidate
          >
            <Grid item>
              <TextField
                  fullWidth
                  required
                  id="username"
                  label="Prisijungimo vardas"
                  name="username"
                  autoFocus
                  onChange={() => setUsernameError(false)}
                  error={usernameError}
                  helperText={(usernameError
                      && 'Prisijungimo vardas turi būti tarp 2 ir 20 simbolių')
                  || ' '}
              />
            </Grid>

            <Grid item>
              <TextField
                  fullWidth
                  name="password"
                  label="Slaptažodis"
                  type="password"
                  id="password"
                  autoComplete="current-password"
                  onChange={() => setPasswordError(false)}
                  error={passwordError}
                  helperText={(passwordError
                      && 'Slaptažodis turi būti pateiktas') || ' '}
              />
            </Grid>

            <Grid item>
              <LoadingButton
                  type="submit"
                  fullWidth
                  variant="contained"
                  loading={loading}
                  sx={{my: 1}}
              >
                Prisijungti
              </LoadingButton>
            </Grid>
          </Grid>

          <Grid item>
            <Link to={'/register'}>
              Neturite paskyros? Registruokitės
            </Link>
          </Grid>
        </Grid>

        <Snackbar
            open={showToast}
            autoHideDuration={3000}
            onClose={() => setShowToast(false)}
            anchorOrigin={{horizontal: 'center', vertical: 'bottom'}}
        >
          <Alert onClose={() => setShowToast(false)}
                 severity="error"
                 variant={'filled'}
                 elevation={6}
          >
            Neteisingi prisijungimo duomenys
          </Alert>
        </Snackbar>
      </Container>
  );
}
