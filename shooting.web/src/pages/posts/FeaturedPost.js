import React from 'react';
import Paper from '@mui/material/Paper';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import {useNavigate} from 'react-router-dom';
import Box from '@mui/material/Box';
import StarIcon from '@mui/icons-material/Star';

export default function FeaturedPost({ post }) {
  const navigate = useNavigate();

  return (
      <Paper
          sx={{
            position: 'relative',
            backgroundColor: 'grey.800',
            color: '#fff',
            mb: 4,
            backgroundSize: 'cover',
            backgroundRepeat: 'no-repeat',
            backgroundPosition: 'center',
            backgroundImage: `url(${post.imageUrl})`,
          }}
          onClick={() => navigate(`/posts/${post.id}`)}
      >
        {/* Increase the priority of the background image */}
        <img style={{ display: 'none' }} src={post.imageUrl} alt={post.imageLabel} />
        <Box
            sx={{
              position: 'absolute',
              top: 0,
              bottom: 0,
              right: 0,
              left: 0,
              backgroundColor: 'rgba(0,0,0,.3)',
            }}
        />
        <Grid container>
          <Grid item md={6}>
            <Box
                sx={{
                  position: 'relative',
                  p: { xs: 3, md: 6 },
                  pr: { md: 0 },
                }}
            >
              <Typography component="h1" variant="h3" color="inherit" gutterBottom>
                <StarIcon fontSize={'large'} /> {post.title}
              </Typography>
              <Typography variant="h5" color="inherit" paragraph>
                {post.description}
              </Typography>
              <Typography variant={'body1'}>
                Skaityti toliau...
              </Typography>
            </Box>
          </Grid>
        </Grid>
      </Paper>
  );
}
