import React, {useState} from 'react';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import Grid from '@mui/material/Grid';
import Drawer from '@mui/material/Drawer';
import useMediaQuery from '@mui/material/useMediaQuery';
import {useNavigate} from 'react-router-dom';
import List from '@mui/material/List';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import Newspaper from '@mui/icons-material/Newspaper';
import Event from '@mui/icons-material/Event';
import {ListItemButton} from '@mui/material';
import AccountButton from './AccountButton';
import Button from '@mui/material/Button';

const links = [
  {
    'name': 'Straipsniai',
    'path': '/',
    'icon': <Newspaper />
  },
  {
    'name': 'Varžybų kalendorius',
    'path': '/competitions',
    'icon': <Event />
  },
];

export default function Header() {
  const navigate = useNavigate();
  const [open, setOpen] = useState(false);
  const small = useMediaQuery(theme => theme.breakpoints.down('md'));

  const openPage = (path) => () => {
    setOpen(false);
    navigate(path);
  };

  return (
      <>
        <AppBar position="sticky">
          <Toolbar>
            <Grid container
                  justifyContent={'space-between'}
                  alignItems={'center'}>
              {small &&
              <Grid item>
                <IconButton
                    size="large"
                    edge="start"
                    color="inherit"
                    aria-label="menu"
                    onClick={() => setOpen(!open)}
                >
                  <MenuIcon />
                </IconButton>
              </Grid>
              }

              <Grid item>
                <Typography variant="h6">
                  Sportinio šaudymo klubas
                </Typography>
              </Grid>

              {!small &&
              <Grid item
                    xs={'auto'}
                    container
                    spacing={6}
                    justifyContent={'center'}
                    alignItems={'center'}>
                {links.map(link => (
                    <Grid item key={link.name}>
                      <Button onClick={() => navigate(link.path)}
                              variant={'text'}
                              color={'inherit'}
                      >
                        {link.name}
                      </Button>
                    </Grid>
                ))}
              </Grid>}

              <Grid item xs={2} lg={1}>
                <AccountButton />
              </Grid>
            </Grid>
          </Toolbar>
        </AppBar>

        <Drawer open={open} onClose={() => setOpen(false)}>
          <Toolbar />
          <List>
            {links.map((link) => (
                <ListItemButton key={link.name}
                                onClick={openPage(link.path)}
                                sx={{pr: 10}}
                >
                  <ListItemIcon>{link.icon}</ListItemIcon>
                  <ListItemText primary={link.name} />
                </ListItemButton>
            ))}
          </List>
        </Drawer>
      </>
  );
}
