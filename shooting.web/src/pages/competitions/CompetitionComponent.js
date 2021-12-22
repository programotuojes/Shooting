import React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import CardActionArea from '@mui/material/CardActionArea';
import {useNavigate} from 'react-router-dom';

export default function CompetitionComponent({competition}) {
  const navigate = useNavigate();

  return (
      <CardActionArea onClick={() => navigate(`/competitions/${competition.id}`)}>
        <Card sx={{display: 'flex'}}>
          <CardContent sx={{flex: 1}}>
            <Typography variant="h4">
              {competition.name}
            </Typography>

            <Typography variant="subtitle1" color="text.secondary">
              Nuo {competition.dateFrom.substring(0, 10)}
            </Typography>

            <Typography variant="subtitle1" color="text.secondary">
              Iki {competition.dateTo.substring(0, 10)}
            </Typography>
          </CardContent>
        </Card>
      </CardActionArea>
  );
}
