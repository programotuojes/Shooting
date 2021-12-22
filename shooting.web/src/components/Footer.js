import React from 'react';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import PhoneIcon from '@mui/icons-material/Phone';
import EmailIcon from '@mui/icons-material/Email';
import LocationOnIcon from '@mui/icons-material/LocationOn';
import {Link} from '@mui/material';
import useMediaQuery from '@mui/material/useMediaQuery';

export default function Footer() {
  const small = useMediaQuery(theme => theme.breakpoints.down('sm'));

  return (
      <Box component="footer"
           position={'absolute'}
           bottom={0}
           height={small ? '340px' : '170px'}
           width={'100%'}
           padding={3}
           marginTop={3}
           bgcolor={'gainsboro'}
      >
        <Container maxWidth="md">
          <Grid container
                spacing={2}>

            <Grid
                container
                direction={'column'}
                spacing={1}
                item
                sm={6}
                xs={12}
            >

              <Grid item>
                <Typography variant="h6">
                  Sportinio šaudymo klubas
                </Typography>
              </Grid>

              <Grid item>
                <Typography variant="body1">
                  Kviečiame apsilankyti. Sportuojame naudojant pneumatinius šautuvus ir pan.
                </Typography>
              </Grid>

              <Grid item>
                <Typography variant="subtitle1">
                  Sukūrė Gustas Klevinskas
                </Typography>
              </Grid>
            </Grid>

            <Grid
                container
                direction={'column'}
                item
                sm={6}
                xs={12}
            >
              <Grid item>
                <Typography variant="h6">
                  Kontaktai
                </Typography>
              </Grid>

              <Grid item container justifyContent='flex-start' alignItems='center'>
                <Grid item xs={1} pt='5px'>
                  <PhoneIcon fontSize={'small'} />
                </Grid>
                <Grid item xs={11}>
                  <Link href='tel: +370 611 11111' color='#000'>
                    +370 611 11111
                  </Link>
                </Grid>

                <Grid item xs={1} pt='5px'>
                  <EmailIcon fontSize={'small'} />
                </Grid>
                <Grid item xs={11}>
                  <Link href='mailto: guskle@ktu.lt'  color='#000'>
                    guskle@ktu.lt
                  </Link>
                </Grid>

                <Grid item xs={1} pt='5px'>
                  <LocationOnIcon fontSize={'small'} />
                </Grid>
                <Grid item xs={11}>
                  <Link color='#000'>
                    Studentų g. 48, Kaunas
                  </Link>
                </Grid>
              </Grid>


            </Grid>

          </Grid>
        </Container>
      </Box>
  );
}
