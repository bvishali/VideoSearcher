import React, {useState} from 'react';
import Style from './BaseLayout.module.scss'
import Navbar from "./Navbar";
import {Route, Routes} from "react-router-dom";
import {Box, Grid} from "@mui/material";
import FormNew from './formc/FormNew';

export default function BaseLayout() {
   let [darkMode, setDarkMode] = useState(false);

   function handleClick() {
      setDarkMode(!darkMode);
   }

   return (
      <Box className={darkMode ? Style.dark : Style.light}  >
         <Grid container display={'flex'} flexDirection={'column'} minHeight={'100vh'}>
            <Grid item>
               <Navbar darkMode={darkMode} handleClick={handleClick}/>
            </Grid>
            <br/>
            <Grid container display={'flex'} flexDirection={'column'} minHeight={'300vh'}>
               <Grid item >
                  <FormNew />
               </Grid>
            </Grid>
         </Grid>
      </Box>
   )
}

