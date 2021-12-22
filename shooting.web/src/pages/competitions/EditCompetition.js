import React, {useEffect, useState} from 'react';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import {ROLE_ADMIN, STORE_ROLE, STORE_TOKEN, URL} from '../../Constants';
import {useNavigate, useParams} from 'react-router-dom';
import Typography from '@mui/material/Typography';
import CircularProgress from '@mui/material/CircularProgress';
import TextField from '@mui/material/TextField';
import LoadingButton from '@mui/lab/LoadingButton';
import {
  Alert,
  Table, TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow
} from '@mui/material';
import Snackbar from '@mui/material/Snackbar';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import Paper from '@mui/material/Paper';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import IconButton from '@mui/material/IconButton';

export default function EditCompetition() {
  const navigate = useNavigate();
  const { competitionId } = useParams();

  const isAdmin = sessionStorage.getItem(STORE_ROLE) === ROLE_ADMIN;
  const emptyNewCompetitor = {
    firstName: '',
    lastName: '',
    birthYear: '',
    result: null,
    id: 0
  };

  const [competition, setCompetition] = useState(undefined);
  const [submitting, setSubmitting] = useState(false);

  const [nameError, setNameError] = useState(false);
  const [dateFromError, setDateFromError] = useState(false);
  const [dateFromErrorLabel, setDateFromErrorLabel] = useState('');
  const [dateToError, setDateToError] = useState(false);

  const [loading, setLoading] = useState(false);
  const [toastMessage, setToastMessage] = useState('');
  const [toastColor, setToastColor] = useState('success');

  const [competitors, setCompetitors] = useState([]);
  const [newCompetitor, setNewCompetitor] = useState(emptyNewCompetitor);
  const [competitorModal, setCompetitorModal] = useState(false);
  const [firstNameError, setFirstNameError] = useState(false);
  const [lastNameError, setLastNameError] = useState(false);
  const [birthYearError, setBirthYearError] = useState(false);

  const handleSubmit = (event) => {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);
    const data = {
      name: formData.get('name'),
      dateFrom: formData.get('dateFrom'),
      dateTo: formData.get('dateTo'),
      competitors
    };

    const nameErr = data.name < 2 || data.name > 50;
    const dateFromErr = data.dateFrom.length === 0 || data.dateFrom > data.dateTo;
    const dateToErr = data.dateTo.length === 0;

    setNameError(nameErr);
    setDateFromError(dateFromErr);
    setDateFromErrorLabel(data.dateFrom.length === 0 ? 'Pradžios data turi būti pateikta' : 'Pradžios data turi būti prieš pabaigos datą');
    setDateToError(dateToErr);

    if (nameErr || dateFromErr || dateToErr) return;

    setSubmitting(true);
    const path = competitionId ? `${URL}/competitions/${competitionId}` : `${URL}/competitions`;
    fetch(path, {
      method: competitionId ? 'PUT' : 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${sessionStorage.getItem(STORE_TOKEN)}`
      },
      body: JSON.stringify(data)
    })
    .then(res => {
      if (res.ok) {
        navigate(-1);
      } else {
        return Promise.reject(res.status);
      }
    })
    .catch(err => {
      setToastColor('error');
      setToastMessage(competitionId && err === 403
        ? 'Keisti varžybas gali tik tas administratorius'
        : 'Tinklo klaida, bandykite dar kartą');
      setSubmitting(false);
    });
  };

  const handleNewCompetitor = () => {
    const firstErr = newCompetitor.firstName.length < 1 || newCompetitor.firstName.length > 30;
    const lastErr = newCompetitor.lastName.length < 1 || newCompetitor.lastName.length > 30;
    const birthErr = newCompetitor.birthYear < 1800 || newCompetitor.birthYear > new Date().getFullYear();

    setFirstNameError(firstErr);
    setLastNameError(lastErr);
    setBirthYearError(birthErr);
    if (firstErr || lastErr || birthErr) return;

    if (newCompetitor.existing) {
      competitors.forEach(x => {
        if (x.id === newCompetitor.id) {
          x.firstName = newCompetitor.firstName;
          x.lastName = newCompetitor.lastName;
          x.birthYear = newCompetitor.birthYear;
          x.result = newCompetitor.result;
        }
      })
    } else {
      setCompetitors(competitors.concat({...newCompetitor, id: Date.now()}));
    }

    setNewCompetitor(emptyNewCompetitor);
    setCompetitorModal(false);
    setFirstNameError(false);
    setLastNameError(false);
    setBirthYearError(false);
  }

  useEffect(() => {
    if (!isAdmin) navigate('/');

    if (competitionId) {
      setLoading(true);

      fetch(`${URL}/competitions/${competitionId}`)
      .then(res => res.json())
      .then(data => {
        console.log(data);
        setCompetition(data);
        setCompetitors(data.competitors);
        setLoading(false);
      })
      .catch(() => navigate(-1));
    }
  }, [navigate, isAdmin, competitionId]);

  return (
      <Container maxWidth={'md'}>
        <Grid container
          spacing={3}
          component="form"
          onSubmit={handleSubmit}
          noValidate
        >

          <Grid item xs={12}>
            <Typography variant={'h4'} textAlign={'center'}>{competitionId
                ? 'Varžybų redagavimas'
                : 'Naujų varžybų registravimas'}
            </Typography>
          </Grid>

          {(competitionId && loading) && <Grid item alignSelf={'center'}><CircularProgress /></Grid>}

          {!loading &&
              <>
                <Grid item xs={12}>
                  <TextField
                      fullWidth
                      required
                      id="name"
                      label="Pavadinimas"
                      name="name"
                      onChange={() => setNameError(false)}
                      defaultValue={competition ? competition.name : ''}
                      error={nameError}
                      helperText={nameError
                          ? 'Pavadinimas turi būti tarp 2 ir 50 simbolių'
                          : ' '}
                  />
                </Grid>

                <Grid item xs={6}>
                  <TextField
                      fullWidth
                      required
                      id="dateFrom"
                      label="Pradžios data"
                      name="dateFrom"
                      type={"date"}
                      InputLabelProps={{shrink: true}}
                      onChange={() => setDateFromError(false)}
                      defaultValue={competition ? competition.dateFrom.substring(0, 10) : ''}
                      error={dateFromError}
                      helperText={dateFromError
                          ? dateFromErrorLabel
                          : ' '}
                  />
                </Grid>

                <Grid item xs={6}>
                  <TextField
                      fullWidth
                      required
                      id="dateTo"
                      label="Pabaigos data"
                      name="dateTo"
                      type={"date"}
                      InputLabelProps={{shrink: true}}
                      defaultValue={competition ? competition.dateTo.substring(0, 10) : ''}
                      onChange={() => setDateToError(false)}
                      error={dateToError}
                      helperText={dateToError
                          ? 'Pabaigos data turi būti pateikta'
                          : ' '}
                  />
                </Grid>

                <Grid item xs={12}>
                  {(competitors.length === 0 &&
                  <Typography variant={'body1'} align='center'>Nėra pridėta dalyvių</Typography>) ||
                  <TableContainer component={Paper}>
                    <Table>
                      <TableHead sx={{backgroundColor: 'gainsboro'}}>
                        <TableRow>
                          <TableCell>Vardas</TableCell>
                          <TableCell>Pavardė</TableCell>
                          <TableCell align='right'>Gimimo metai</TableCell>
                          <TableCell align='right'>Rezultatas</TableCell>
                          <TableCell width='50px' />
                          <TableCell width='50px' />
                        </TableRow>
                      </TableHead>

                      <TableBody>
                        {competitors.map(competitor =>
                        <TableRow key={competitor.id}>
                          <TableCell>{competitor.firstName}</TableCell>
                          <TableCell>{competitor.lastName}</TableCell>
                          <TableCell align='right'>{competitor.birthYear}</TableCell>
                          <TableCell align='right'>{competitor.result === null ? '-' : competitor.result}</TableCell>
                          <TableCell align='right'>
                            <IconButton onClick={() => {
                              setNewCompetitor({...competitor, existing: true});
                              setCompetitorModal(true);
                            }}>
                              <EditIcon />
                            </IconButton>
                          </TableCell>
                          <TableCell align='right'>
                            <IconButton onClick={() => setCompetitors(competitors.filter(x => x.id !== competitor.id))}>
                              <DeleteIcon />
                            </IconButton>
                          </TableCell>
                        </TableRow>)}
                      </TableBody>
                    </Table>
                  </TableContainer>}
                </Grid>

                <Grid item xs={6} textAlign={'right'}>
                  <LoadingButton
                      type="submit"
                      variant="contained"
                      loading={submitting}
                      sx={{my: 1}}
                  >
                    Išsaugoti
                  </LoadingButton>
                </Grid>

                <Grid item xs={6} textAlign={'left'}>
                  <LoadingButton
                      variant="contained"
                      color={'secondary'}
                      loading={submitting}
                      sx={{my: 1}}
                      onClick={() => setCompetitorModal(true)}
                  >
                    Pridėti dalyvį
                  </LoadingButton>
                </Grid>
              </>
          }
        </Grid>

        <Snackbar
            open={!!toastMessage}
            autoHideDuration={3000}
            onClose={() => setToastMessage('')}
            anchorOrigin={{horizontal: 'center', vertical: 'bottom'}}
        >
          <Alert onClose={() => setToastMessage('')}
                 severity={toastColor}
                 variant={'filled'}
                 elevation={6}
          >
            {toastMessage}
          </Alert>
        </Snackbar>

        <Dialog open={competitorModal} onClose={() => setCompetitorModal(false)} fullWidth>
          <DialogTitle>Dalyvio pridėjimas</DialogTitle>
          <DialogContent sx={{py: '10px !important'}}>
            <Grid container columnSpacing={4} rowSpacing={2}>
              <Grid item xs={12} sm={6}>
                <TextField
                    autoFocus
                    value={newCompetitor.firstName}
                    onChange={e => {
                      setNewCompetitor({...newCompetitor, firstName: e.target.value});
                      setFirstNameError(false);
                    }}
                    id="firstName"
                    label="Vardas"
                    fullWidth
                    error={firstNameError}
                    helperText={firstNameError
                        ? 'Vardas turi būti tarp 1 ir 30 simbolių'
                        : ' '}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                    value={newCompetitor.lastName}
                    onChange={e => {
                      setNewCompetitor({...newCompetitor, lastName: e.target.value});
                      setLastNameError(false);
                    }}
                    id="lastName"
                    label="Pavardė"
                    fullWidth
                    error={lastNameError}
                    helperText={lastNameError
                        ? 'Pavardė turi būti tarp 1 ir 30 simbolių'
                        : ' '}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                    value={newCompetitor.birthYear}
                    onChange={e => {
                      setNewCompetitor({...newCompetitor, birthYear: e.target.value});
                      setBirthYearError(false);
                    }}
                    id="birthYear"
                    label="Gimimo metai"
                    type={'number'}
                    fullWidth
                    error={birthYearError}
                    helperText={birthYearError
                        ? `Gimimo metai turi būti tarp 1800 ir ${new Date().getFullYear()}`
                        : ' '}
                />
              </Grid>
              <Grid item xs={12} sm={6}>
                <TextField
                    value={newCompetitor.result === null ? '' : newCompetitor.result}
                    onChange={e => {
                      setNewCompetitor({...newCompetitor, result: e.target.value});
                    }}
                    id="result"
                    label="Rezultatas"
                    type={'number'}
                    fullWidth
                />
              </Grid>
            </Grid>

          </DialogContent>
          <DialogActions>
            <Button color={'error'} onClick={() => {
              setCompetitorModal(false);
              setNewCompetitor(emptyNewCompetitor);
              setFirstNameError(false);
              setLastNameError(false);
              setBirthYearError(false);
            }}>
              Atšaukti
            </Button>
            <Button onClick={handleNewCompetitor}>
              Pridėti
            </Button>
          </DialogActions>
        </Dialog>
      </Container>
  );
}
