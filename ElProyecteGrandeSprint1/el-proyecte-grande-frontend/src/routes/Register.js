import React from 'react';
import PropTypes from "prop-types";
import UserForm from "../components/UserForm";

const Register = ({postData}) => {


  const SendDataToBackEnd = async (inputs) => {
       return await postData("User", inputs);
  }


  return (
    <UserForm postData={postData} SendDataToBackEnd={SendDataToBackEnd} Page={"Register"}></UserForm>
  )
}
Register.prototype = {
    postData: PropTypes.func
}


export default Register