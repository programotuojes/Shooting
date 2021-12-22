import React, {useEffect, useState} from 'react';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import CircularProgress from '@mui/material/CircularProgress';
import {ROLE_ADMIN, STORE_ROLE, URL} from '../../Constants';
import {useNavigate} from 'react-router-dom';
import CompetitionComponent from './CompetitionComponent';

export default function Competitions() {
  const navigate = useNavigate();

  const [competitions, setCompetitions] = useState([]);
  const [loading, setLoading] = useState(true);

  const isAdmin = sessionStorage.getItem(STORE_ROLE) === ROLE_ADMIN;

  useEffect(() => {
    fetch(`${URL}/competitions`)
    .then(res => res.ok ? res.json() : Promise.reject())
    .then(data => {
      setCompetitions(data);
      setLoading(false);
    })
    .catch(err => console.log(err));
  }, []);

  const Content = () => {
    if (competitions.length === 0) {
      return (
          <Grid item>
            <Typography fontSize={25}>Varžybų nerasta</Typography>
          </Grid>
      );
    }

    return competitions.map(competition => (
      <Grid item xs={12} key={competition.id}>
        <CompetitionComponent competition={competition} />
      </Grid>
    ));
  };

  return (
      <Container maxWidth={'md'}>
        <Grid container justifyContent={'center'} spacing={6}>
          <Grid container
                direction={'column'}
                alignItems={'center'}
                item
                xs={12}
          >
            <Grid item>
              <Typography variant="h3" gutterBottom align={'center'}>
                Varžybos
              </Typography>
            </Grid>

            {isAdmin &&
            <Grid item>
              <Button onClick={() => navigate('/competitions/new')}
                      variant={'contained'}
              >
                Registruoti naujas varžybas
              </Button>
            </Grid>}
          </Grid>

          {loading && <Grid item><CircularProgress /></Grid>}

          {!loading && <Content />}
        </Grid>
      </Container>
  );
}
