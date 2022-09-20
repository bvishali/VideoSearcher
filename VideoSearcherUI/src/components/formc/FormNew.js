import TextField from "@material-ui/core/TextField";
import React, { useState } from "react";
import { Button, Paper } from "@material-ui/core";
import {Box} from "@mui/material";

export default function FormNew() {
 
  const [videoUrlValue, setVideoUrlValue] = useState("");
  const [keywordValue, setKeywordValue] = useState("");
  const [message, setMessage] = useState("");

  const onVideoUrlChange = (e) => {setMessage(""); setVideoUrlValue(e.target.value);}
  const onKeywordsChange = (e) => {setMessage(""); setKeywordValue(e.target.value);}
  const handleSubmit = async (e) => {
   e.preventDefault();
      try {
      let res = await fetch("https://httpbin.org/post", {
         method: "POST",
         body: JSON.stringify({
            videoUrlValue: videoUrlValue,
            keywordValue: keywordValue
         }),
      });
      let resJson = await res.json();
      if (res.status === 200) {
         setVideoUrlValue("");
         setKeywordValue("");
         setMessage("Search Started Successfully");
      } else {
         setMessage("Some error occured");
      }
      } catch (err) {
      console.log(err);
      }
   }
  const handleReset = () => { setVideoUrlValue(""); setKeywordValue(""); setMessage("");}
 
   
     return (
      <div>
      <Box component={'nav'} width={'100%'}>
         <Box component={'ul'} display={'flex'} justifyContent={'center'} alignItems={'center'}>
            <Paper elevation={3}>
            
            <TextField
               label="Video Url"
               onChange={onVideoUrlChange}
               value={videoUrlValue}
            />
            <br/>
            <TextField
               label="Keywords"
               onChange={onKeywordsChange}
               value={keywordValue}
            />
            <br/>
            <Button variant="contained" onClick={handleSubmit}>Submit</Button>
            <Button  variant="contained" onClick={handleReset}>Reset</Button>
            </Paper>
         </Box>
         
      </Box>
       <div >{message ?  
       <Box component={'nav'} width={'100%'}>
       <Box component={'ul'} display={'flex'} justifyContent={'center'} alignItems={'center'}>
       <iframe width="640" height="360" src="https://msit.microsoftstream.com/embed/video/96d10840-98dc-997d-af94-f1eca6251b88?autoplay=false&showinfo=true">

       </iframe>
        {/*https://mui.com/x/react-data-grid/*/}
       </Box> 
         </Box> : null}
       </div> 
     
      
      </div>
     );
   
 }
