import React from 'react';
import Typography from '@mui/material/Typography';
import Card from '@mui/material/Card';
import CardActionArea from '@mui/material/CardActionArea';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import {useNavigate} from 'react-router-dom';

export default function PostComponent({post}) {
  const navigate = useNavigate();

  return (
      <CardActionArea onClick={() => navigate(`/posts/${post.id}`)}>
        <Card sx={{display: 'flex'}}>
          <CardContent sx={{flex: 1}}>
            <Typography variant="h4">
              {post.title}
            </Typography>

            <Typography variant="subtitle1" color="text.secondary">
              {post.createdOn.substring(0, 10)} | {post.createdBy}
            </Typography>

            <Typography variant="h6">
              {post.description}
            </Typography>

            <Typography variant="subtitle1" color="text.secondary">
              Skaityti toliau...
            </Typography>
          </CardContent>

          <CardMedia
              component="img"
              sx={{width: 160, display: {xs: 'none', sm: 'block'}}}
              image={post.imageUrl}
              alt={post.imageLabel}
          />
        </Card>
      </CardActionArea>
  );
}
