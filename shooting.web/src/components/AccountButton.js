import React, {useState} from 'react';
import Button from '@mui/material/Button';
import {STORE_ROLE, STORE_TOKEN, STORE_USERNAME} from '../Constants';
import {useNavigate} from 'react-router-dom';
import PersonIcon from '@mui/icons-material/Person';


export default function AccountButton() {
  const navigate = useNavigate();

  const username = sessionStorage.getItem(STORE_USERNAME);
  const [hovering, setHovering] = useState(false);

  const logout = () => {
    sessionStorage.removeItem(STORE_USERNAME);
    sessionStorage.removeItem(STORE_ROLE);
    sessionStorage.removeItem(STORE_TOKEN);
    window.location.reload();
  }

  if (username) {
    return (
        <Button color="inherit"
                onClick={logout}
                startIcon={!hovering && <PersonIcon />}
                variant={'outlined'}
                onMouseEnter={() => setHovering(true)}
                onMouseLeave={() => setHovering(false)}
                fullWidth
        >
          {hovering ? 'Atsijungti' : username}
        </Button>
    );
  } else {
    return (
        <Button color="inherit"
                onClick={() => navigate('/login')}
                variant={'outlined'}
                fullWidth
        >
          Prisijungti
        </Button>
    );
  }
}
