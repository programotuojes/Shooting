import React, {useEffect, useState} from 'react';
import PostComponent from './PostComponent';
import Grid from '@mui/material/Grid';
import CircularProgress from '@mui/material/CircularProgress';
import Typography from '@mui/material/Typography';
import {ROLE_ADMIN, STORE_ROLE, URL} from '../../Constants';
import Snackbar from '@mui/material/Snackbar';
import {Alert, Container} from '@mui/material';
import FeaturedPost from './FeaturedPost';
import Button from '@mui/material/Button';
import {useNavigate} from 'react-router-dom';

export default function Posts() {
  const navigate = useNavigate();

  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(false);
  const [showToast, setShowToast] = useState(false);

  const isAdmin = sessionStorage.getItem(STORE_ROLE) === ROLE_ADMIN;

  useEffect(() => {
    fetch(`${URL}/posts`)
    .then(res => res.json())
    .then(data => {
      setPosts(data);
      setLoading(false);
    })
    .catch(() => {
      setError(true);
      setShowToast(true);
    });
  }, []);

  const Content = () => {
    if (posts.length === 0) {
      return (
          <Grid item>
            <Typography fontSize={25}>Straipsnių nerasta</Typography>
          </Grid>
      );
    }

    return (
        <>
          <Grid item xs={12}>
            <FeaturedPost post={posts[0]} />
          </Grid>

          {posts.slice(1).map(post => (
              <Grid item xs={12} key={post.id}>
                <PostComponent post={post} />
              </Grid>))}
        </>
    );
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
                Straipsniai
              </Typography>
            </Grid>

            {isAdmin &&
            <Grid item>
              <Button onClick={() => navigate('/posts/new')}
                      variant={'contained'}
              >
                Naujas straipsnis
              </Button>
            </Grid>}
          </Grid>

          {(loading && !error) && <Grid item><CircularProgress /></Grid>}

          {(!loading && !error) && <Content />}

        </Grid>

        <Snackbar
            open={showToast}
            autoHideDuration={2000}
            onClose={() => setShowToast(false)}
            anchorOrigin={{horizontal: 'center', vertical: 'bottom'}}
        >
          <Alert onClose={() => setShowToast(false)}
                 severity="error"
                 variant={'filled'}
                 elevation={6}
          >
            Nepavyko atsiųsti straipsnių
          </Alert>
        </Snackbar>
      </Container>
  );
}
