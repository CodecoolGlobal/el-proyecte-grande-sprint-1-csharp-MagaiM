import React from 'react';
import PropTypes from "prop-types";
import UserForm from "../components/UserForm";

const Register = ({postData}) => {


  const SendDataToBackEnd = (inputs) =>{
        console.log(postData("User", inputs))
    }


  return (
    <UserForm postData={postData} SendDataToBackEnd={SendDataToBackEnd}></UserForm>
  )
}
Register.prototype = {
    postData: PropTypes.func
}


export default Register