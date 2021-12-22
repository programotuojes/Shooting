import React, {useEffect, useState} from 'react';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import TextField from '@mui/material/TextField';
import LoadingButton from '@mui/lab/LoadingButton';
import {ROLE_ADMIN, STORE_ROLE, STORE_TOKEN, URL} from '../../Constants';
import {useNavigate, useParams} from 'react-router-dom';
import {Alert} from '@mui/material';
import Snackbar from '@mui/material/Snackbar';
import CircularProgress from '@mui/material/CircularProgress';

export default function EditPost() {
  const navigate = useNavigate();
  const {postId} = useParams();

  const [titleError, setTitleError] = useState(false);
  const [descriptionError, setDescriptionError] = useState(false);
  const [bodyError, setBodyError] = useState(false);
  const [imageUrlError, setImageUrlError] = useState(false);
  const [imageLabelError, setImageLabelError] = useState(false);

  const [loading, setLoading] = useState(false);
  const [showToast, setShowToast] = useState(false);

  const [post, setPost] = useState(false);
  const [loadingPost, setLoadingPost] = useState(false);

  const isAdmin = sessionStorage.getItem(STORE_ROLE) === ROLE_ADMIN;

  const handleSubmit = (event) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    const postData = {
      title: formData.get('title'),
      description: formData.get('description'),
      body: formData.get('body'),
      imageUrl: formData.get('imageUrl'),
      imageLabel: formData.get('imageLabel'),
    };

    const titleErr = postData.title.length < 2 || postData.title.length > 100;
    const descriptionErr = postData.description.length < 2
        || postData.description.length > 100;
    const bodyErr = postData.body.length < 2 || postData.body.length > 10000;
    const imageUrlErr = postData.imageUrl.length === 0
        || postData.imageUrl.match(/^(http|https)?:\/\/.+\/.+$/) === null;
    const imageLabelErr = postData.imageLabel.length < 2
        || postData.imageLabel.length > 100;

    setTitleError(titleErr);
    setDescriptionError(descriptionErr);
    setBodyError(bodyErr);
    setImageUrlError(imageUrlErr);
    setImageLabelError(imageLabelErr);

    if (titleErr || descriptionErr || bodyErr || imageUrlErr || imageLabelErr) {
      return;
    }

    const path = postId ? `${URL}/posts/${postId}` : `${URL}/posts`;

    fetch(path, {
      method: postId ? 'PUT' : 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${sessionStorage.getItem(STORE_TOKEN)}`
      },
      body: JSON.stringify(postData)
    })
    .then(res => {
      if (res.ok) {
        navigate(-1);
      } else {
        return Promise.reject();
      }
    })
    .catch(() => {
      setLoading(false);
      setShowToast(true);
    });
  };

  useEffect(() => {
    if (!isAdmin) {
      navigate('/');
    }

    if (postId) {
      setLoadingPost(true);

      fetch(`${URL}/posts/${postId}`)
      .then(res => res.json())
      .then(data => {
        setPost(data);
        setLoadingPost(false);
      })
      .catch(() => navigate(-1));
    }
  }, [navigate, isAdmin, postId]);

  return (
      <Container maxWidth={'md'}>
        <Grid container
              spacing={3}
              direction={'column'}
              component="form"
              onSubmit={handleSubmit}
              noValidate
        >

          <Grid item>
            <Typography variant={'h4'}>{postId
                ? 'Straipsnio redagavimas'
                : 'Naujo straipsnio kūrimas'}
            </Typography>
          </Grid>

          {(postId && loadingPost) && <Grid item alignSelf={'center'}><CircularProgress /></Grid>}

          {!loadingPost && (
              <>
                <Grid item>
                  <TextField
                      fullWidth
                      required
                      id="title"
                      label="Pavadinimas"
                      name="title"
                      onChange={() => setTitleError(false)}
                      defaultValue={post ? post.title : ''}
                      error={titleError}
                      helperText={titleError
                          ? 'Pavadinimas turi būti tarp 2 ir 100 simbolių'
                          : ' '}
                  />
                </Grid>

                <Grid item>
                  <TextField
                      fullWidth
                      required
                      id="description"
                      label="Trumpas aprašas"
                      name="description"
                      onChange={() => setDescriptionError(false)}
                      defaultValue={post ? post.description : ''}
                      error={descriptionError}
                      helperText={descriptionError
                          ? 'Aprašas turi būti tarp 2 ir 100 simbolių'
                          : ' '}
                  />
                </Grid>

                <Grid item>
                  <TextField
                      fullWidth
                      required
                      id="body"
                      label="Turinys"
                      name="body"
                      multiline
                      onChange={() => setBodyError(false)}
                      defaultValue={post ? post.body : ''}
                      error={bodyError}
                      helperText={bodyError
                          ? 'Turinys turi būti tarp 2 ir 10k simbolių'
                          : ' '}
                  />
                </Grid>

                <Grid item>
                  <TextField
                      fullWidth
                      required
                      id="imageUrl"
                      label="Nuotraukos nuoroda"
                      name="imageUrl"
                      type={'url'}
                      onChange={() => setImageUrlError(false)}
                      defaultValue={post ? post.imageUrl : ''}
                      error={imageUrlError}
                      helperText={imageUrlError
                          ? 'Nuoroda į paveiksliuką turi būti pateikta'
                          : ' '}
                  />
                </Grid>

                <Grid item>
                  <TextField
                      fullWidth
                      required
                      id="imageLabel"
                      label="Nuotraukos aprašas"
                      name="imageLabel"
                      onChange={() => setImageLabelError(false)}
                      defaultValue={post ? post.imageLabel : ''}
                      error={imageLabelError}
                      helperText={imageLabelError
                          ? 'Nuotraukos aprašas turi būti tarp 2 ir 100 simbolių'
                          : ' '}
                  />
                </Grid>
              </>)}

          <Grid item alignSelf={'center'}>
            <LoadingButton
                type="submit"
                variant="contained"
                loading={loading}
                sx={{my: 1}}
            >
              Išsaugoti
            </LoadingButton>
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
            Tik administratoriai gali sukurti straipsnius
          </Alert>
        </Snackbar>
      </Container>
  )
}
