import React, {useEffect, useState} from 'react';
import Container from '@mui/material/Container';
import {
  ROLE_ADMIN, STORE_ROLE,
  STORE_TOKEN,
  URL
} from '../../Constants';
import {useNavigate, useParams} from 'react-router-dom';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import CircularProgress from '@mui/material/CircularProgress';
import {
  Alert,
  Divider,
  Table, TableBody,
  TableCell, TableContainer,
  TableHead,
  TableRow
} from '@mui/material';
import Snackbar from '@mui/material/Snackbar';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import Paper from '@mui/material/Paper';

export default function Competition() {
  const {competitionId} = useParams();
  const navigate = useNavigate();

  const isAdmin = sessionStorage.getItem(STORE_ROLE) === ROLE_ADMIN;

  const [competition, setCompetition] = useState({});
  const [loading, setLoading] = useState(true);

  const [toastText, setToastText] = useState('');
  const [toastColor, setToastColor] = useState('error');

  const handleDelete = () => {
    fetch(`${URL}/competitions/${competitionId}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${sessionStorage.getItem(STORE_TOKEN)}`
      },
    })
    .then(res => res.ok ? Promise.resolve() : Promise.reject())
    .then(() => navigate('/competitions'))
    .catch(err => {
      console.log(err);
      setToastText('Nepavyko ištrinti varžybų');
      setToastColor('error');
    });
  };

  useEffect(() => {
    setLoading(true);

    fetch(`${URL}/competitions/${competitionId}`)
    .then(res => res.json())
    .then(data => {
      setCompetition(data);
      setLoading(false);
    })
    .catch(() => navigate(-1));
  }, [isAdmin, competitionId, navigate]);

  return (
      <Container maxWidth={'md'}>
        <Grid container justifyContent={'center'} spacing={2} mb={20}>

          {loading && <Grid item><CircularProgress /></Grid>}

          {!loading && (
              <>
                <Grid item xs={12}>
                  <Typography variant="h3" align={'center'}>
                    {competition.name} {isAdmin &&
                      <>
                      <IconButton onClick={() => navigate(`/competitions/${competitionId}/edit`)}>
                        <EditIcon />
                      </IconButton>
                      <IconButton onClick={handleDelete}>
                        <DeleteIcon />
                      </IconButton>
                      </>
                    }
                  </Typography>
                </Grid>

                <Grid item xs={12}>
                  <Typography variant={"body1"} textAlign={'center'}>
                    Nuo {competition.dateFrom.substring(0, 10)}
                  </Typography>
                </Grid>

                <Grid item xs={12}>
                  <Typography variant={"body1"} textAlign={'center'}>
                    Iki {competition.dateTo.substring(0, 10)}
                  </Typography>
                </Grid>

                <Grid item xs={12} mt={5} mb={2}>
                  <Divider>
                    <Typography variant="h4" align={'center'}>
                      Dalyviai
                    </Typography>
                  </Divider>
                </Grid>

                {competition.competitors.length === 0 &&
                <Grid item xs={12} mb={4}>
                  <Typography variant="body1" align={'center'}>
                    Dalyvių nėra
                  </Typography>
                </Grid>
                }

                <Grid item xs={12}>
                  <TableContainer component={Paper}>
                    <Table>
                      <TableHead sx={{backgroundColor: 'gainsboro'}}>
                        <TableRow>
                          <TableCell>Vardas</TableCell>
                          <TableCell>Pavardė</TableCell>
                          <TableCell align='right'>Gimimo metai</TableCell>
                          <TableCell align='right'>Rezultatas</TableCell>
                        </TableRow>
                      </TableHead>

                      <TableBody>
                        {competition.competitors.map(competitor =>
                            <TableRow key={competitor.id}>
                              <TableCell>{competitor.firstName}</TableCell>
                              <TableCell>{competitor.lastName}</TableCell>
                              <TableCell align='right'>{competitor.birthYear}</TableCell>
                              <TableCell align='right'>{competitor.result === null ? '-' : competitor.result}</TableCell>
                            </TableRow>)}
                      </TableBody>
                    </Table>
                  </TableContainer>
                </Grid>
              </>
          )}

        </Grid>

        <Snackbar
            open={!!toastText}
            autoHideDuration={3000}
            onClose={() => setToastText('')}
            anchorOrigin={{horizontal: 'center', vertical: 'bottom'}}
        >
          <Alert onClose={() => setToastText('')}
                 severity={toastColor}
                 variant={'filled'}
                 elevation={6}
          >
            {toastText}
          </Alert>
        </Snackbar>
      </Container>
  );
}
