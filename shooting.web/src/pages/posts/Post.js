import React, {useEffect, useState} from 'react';
import Container from '@mui/material/Container';
import {
  STORE_TOKEN,
  STORE_USERNAME,
  URL
} from '../../Constants';
import {useNavigate, useParams} from 'react-router-dom';
import Grid from '@mui/material/Grid';
import Typography from '@mui/material/Typography';
import CircularProgress from '@mui/material/CircularProgress';
import Box from '@mui/material/Box';
import {Alert, CardActions, Divider, TextField} from '@mui/material';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import LoadingButton from '@mui/lab/LoadingButton';
import Snackbar from '@mui/material/Snackbar';
import SendIcon from '@mui/icons-material/Send';
import PersonIcon from '@mui/icons-material/Person';
import IconButton from '@mui/material/IconButton';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';

export default function Post() {
  const {postId} = useParams();
  const navigate = useNavigate();

  const loggedIn = sessionStorage.getItem(STORE_TOKEN) !== null;

  const [comment, setComment] = useState('');
  const [commentError, setCommentError] = useState(false);
  const [commentLoading, setCommentLoading] = useState(false);
  const [commentToast, setCommentToast] = useState('');

  const [modalOpen, setModalOpen] = useState(false);
  const [modalCommentId, setModalCommentId] = useState(null);
  const [modalCommentText, setModalCommentText] = useState('');
  const [modalCommentError, setModalCommentError] = useState(false);
  const [modalLoading, setModalLoading] = useState(false);

  const [showDeleteDialog, setShowDeleteDialog] = useState(false);
  const [deleteDialogId, setDeleteDialogId] = useState(null);
  const [loadingDelete, setLoadingDelete] = useState(false);

  const [toastColor, setToastColor] = useState('error');
  const [toastText, setToastText] = useState('');

  const [post, setPost] = useState({});
  const [loading, setLoading] = useState(true);

  const handleUpdate = () => {
    const commentErr = modalCommentText.length < 2 || modalCommentText.length > 100;
    setModalCommentError(commentErr);
    if (commentErr) return;

    setModalLoading(true);

    fetch(`${URL}/posts/${postId}/comments/${modalCommentId}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${sessionStorage.getItem(STORE_TOKEN)}`
      },
      body: JSON.stringify({body: modalCommentText})
    })
    .then(res => res.ok ? res.json() : Promise.reject())
    .then(data => {
      post.comments.forEach(x => { if (x.id === modalCommentId) x.body = data.body; });
      setPost(post);
      setModalLoading(false);

      setToastText('Komentaras sėkmingai atnaujintas');
      setToastColor('success');

      setModalOpen(false);
    })
    .catch(() => {
      setModalLoading(false);
      setModalCommentError(true);
      setModalOpen(false);
    });
  };

  const handleDelete = () => {
    setLoadingDelete(true);

    fetch(`${URL}/posts/${postId}/comments/${deleteDialogId}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${sessionStorage.getItem(STORE_TOKEN)}`
      },
    })
    .then(res => res.ok ? Promise.resolve() : Promise.reject())
    .then(() => {
      post.comments = post.comments.filter(x => x.id !== deleteDialogId);
      setPost(post);
      setLoadingDelete(false);
      setShowDeleteDialog(false);
      setToastText('Komentaras ištrintas');
      setToastColor('success');
    })
    .catch(err => {
      console.log(err);
      setLoadingDelete(false);
      setShowDeleteDialog(false);
    });
  }

  const submitComment = () => {
    const commentErr = comment.length < 2 || comment.length > 100;
    setCommentError(commentErr);
    if (commentErr) return;

    setCommentLoading(true);

    fetch(`${URL}/posts/${postId}/comments`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${sessionStorage.getItem(STORE_TOKEN)}`
      },
      body: JSON.stringify({body: comment})
    })
    .then(res => res.ok ? res.json() : Promise.reject(res.status))
    .then(data => {
      setCommentLoading(false);
      setPost({...post, comments: post.comments.concat(data)});
      setComment('');
    })
    .catch(err => {
      setCommentLoading(false);
      setCommentToast(err === 403
        ? 'Tik prisijungę naudotojai gali palikti komentarą'
        : 'Nepavyko išsaugoti komentaro');
    })
  };

  const handleDeletePost = () => {
    fetch(`${URL}/posts/${postId}`, {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${sessionStorage.getItem(STORE_TOKEN)}`
      },
    })
    .then(res => res.ok ? Promise.resolve() : Promise.reject())
    .then(() => {
      navigate('/');
    })
    .catch(err => {
      console.log(err);
      setToastText('Nepavyko ištrinti straipsnio');
      setToastColor('error');
    });
  };

  useEffect(() => {
    fetch(`${URL}/posts/${postId}`)
    .then(res => res.json())
    .then(data => {
      setPost(data);
      setLoading(false);
    })
    .catch(() => navigate(-1));
  }, [postId, navigate]);

  return (
      <Container maxWidth={'md'}>
        <Grid container justifyContent={'center'} spacing={2} mb={20}>

          {loading && <Grid item><CircularProgress /></Grid>}

          {!loading && (
              <>
                <Grid item xs={12}>
                  <Typography variant="h3" align={'center'}>
                    {post.title} {sessionStorage.getItem(STORE_USERNAME) === post.createdBy &&
                      <>
                      <IconButton onClick={() => navigate(`/posts/${postId}/edit`)}>
                        <EditIcon />
                      </IconButton>
                      <IconButton onClick={handleDeletePost}>
                        <DeleteIcon />
                      </IconButton>
                      </>
                    }
                  </Typography>
                </Grid>

                <Grid item xs={12}>
                  <Typography variant="subtitle1" align={'center'}>
                    {post.createdOn.substring(0, 10)} | {post.createdBy}
                  </Typography>
                </Grid>

                <Grid item sm={12} md={4}>
                  <Box component={"img"}
                       src={post.imageUrl}
                       alt={post.imageLabel}
                       sx={{maxWidth: '1'}} />
                </Grid>

                <Grid item sm={12} md={8}>
                  <Typography variant={"body1"} whiteSpace={'pre-line'}>
                    {post.body}
                  </Typography>
                </Grid>

                <Grid item xs={12} mt={5} mb={2}>
                  <Divider>
                    <Typography variant="h4" align={'center'}>
                      Komentarai
                    </Typography>
                  </Divider>
                </Grid>

                {post.comments.length === 0 &&
                <Grid item xs={12} mb={4}>
                  <Typography variant="body1" align={'center'}>
                    Komentarų nėra
                  </Typography>
                </Grid>
                }

                {post.comments.map(comment => (
                    <Grid item xs={12} key={comment.id}>

                      <Card elevation={4}>
                        <CardContent>
                          <Grid container rowSpacing={2} columnSpacing={1}>
                            <Grid item xs={"auto"}>
                              <PersonIcon />
                            </Grid>

                            <Grid item xs>
                              <Typography variant={"h5"} textAlign={'left'}>
                                 {comment.createdBy}
                              </Typography>
                            </Grid>

                            <Grid item xs>
                              <Typography variant={"subtitle1"} textAlign={'right'}>
                                {comment.createdOn.substring(0, 10)} {comment.createdOn.substring(11, 16)}
                              </Typography>
                            </Grid>

                            <Grid item xs={12}>
                              <Typography variant={"body1"} whiteSpace={'pre-line'}>
                                {comment.body}
                              </Typography>
                            </Grid>
                          </Grid>
                        </CardContent>

                        {comment.createdBy === sessionStorage.getItem(STORE_USERNAME) &&
                        <CardActions>
                          <IconButton onClick={() => {
                            setModalCommentId(comment.id);
                            setModalCommentText(comment.body);
                            setModalOpen(true);
                          }}>
                            <EditIcon />
                          </IconButton>
                          <IconButton onClick={() => {
                            setDeleteDialogId(comment.id);
                            setShowDeleteDialog(true);
                          }}>
                            <DeleteIcon />
                          </IconButton>
                        </CardActions>
                        }
                      </Card>

                    </Grid>
                ))}

                <Grid item xs={12} container alignItems={'center'}>
                  <Grid item xs={12} sm={9}>
                    <TextField multiline
                               placeholder={loggedIn
                                 ? 'Įveskite savo komentarą...'
                                 : 'Tik prisijungę naudotojai gali palikti komentarą'}
                               value={comment}
                               onChange={e => {
                                 setComment(e.target.value);
                                 setCommentError(false);
                               }}
                               minRows={3}
                               fullWidth
                               disabled={!loggedIn}
                               error={commentError}
                               helperText={commentError
                                   ? 'Komentaras turi būti tarp 2 ir 100 simbolių'
                                   : ' '}
                    />
                  </Grid>

                  <Grid item xs={12} sm={3} textAlign={'center'}>
                    <LoadingButton
                        sx={{bottom: '12px'}}
                        startIcon={<SendIcon/>}
                        disabled={!loggedIn}
                        variant="contained"
                        loading={commentLoading}
                        onClick={submitComment}
                    >
                      Išsaugoti
                    </LoadingButton>
                  </Grid>
                </Grid>
              </>
          )}

        </Grid>

        <Snackbar
            open={!!commentToast}
            autoHideDuration={3000}
            onClose={() => setCommentToast('')}
            anchorOrigin={{horizontal: 'center', vertical: 'bottom'}}
        >
          <Alert onClose={() => setCommentToast('')}
                 severity="error"
                 variant={'filled'}
                 elevation={6}
          >
            {commentToast}
          </Alert>
        </Snackbar>

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

        <Dialog open={modalOpen} onClose={() => setModalOpen(false)} fullWidth>
          <DialogTitle>Komentaro atnaujinimas</DialogTitle>
          <DialogContent sx={{py: '10px !important'}}>
            <TextField
                autoFocus
                value={modalCommentText}
                onChange={e => {
                  setModalCommentText(e.target.value);
                  setModalCommentError(false);
                }}
                id="name"
                label="Atnaujintas komentaras"
                fullWidth
                error={modalCommentError}
                helperText={modalCommentError
                    ? 'Komentaras turi būti tarp 2 ir 100 simbolių'
                    : ' '}
            />
          </DialogContent>
          <DialogActions>
            <Button color={'error'} onClick={() => {
              setModalOpen(false);
              setModalCommentError(false);
            }}>
              Atšaukti
            </Button>
            <LoadingButton onClick={handleUpdate} loading={modalLoading}>Išsaugoti</LoadingButton>
          </DialogActions>
        </Dialog>

        <Dialog open={showDeleteDialog} onClose={() => setShowDeleteDialog(false)} fullWidth>
          <DialogTitle>Ar tikrai norite ištrinti šį komentarą?</DialogTitle>
          <DialogActions>
            <Button color={'error'} onClick={() => {
              setShowDeleteDialog(false);
              setDeleteDialogId(null);
            }}>
              Atšaukti
            </Button>
            <LoadingButton onClick={handleDelete} loading={loadingDelete}>Ištrinti</LoadingButton>
          </DialogActions>
        </Dialog>
      </Container>
  );
}
