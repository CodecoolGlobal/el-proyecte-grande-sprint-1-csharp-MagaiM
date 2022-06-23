import React from 'react';
import { useState } from 'react';
import PropTypes from "prop-types";

const UserForm = ({SendDataToBackEnd}) => {

  const [inputs, setInputs] = useState({});

  const handleChange = (event) => {
    const UserName = event.target.name;
    const Password = event.target.value;
    setInputs(values => ({...values, [UserName]: Password}))
  }

  const handleSubmit = (event) => {
    event.preventDefault();
    console.log(inputs);
    SendDataToBackEnd(inputs);
  }

  return (
    <form onSubmit={handleSubmit}>
      <label>Username
      <input
        type="text"
        name="username"
        value={inputs.username || ""}
        onChange={handleChange}
      />
      </label>
      <label>Password
        <input
          type="text"
          name="password"
          value={inputs.password || ""}
          onChange={handleChange}
        />
        </label>
        <input type="submit" />
    </form>
  )
}
UserForm.prototype = {
    postData: PropTypes.func,
    SendDataToBackEnd: PropTypes.func
}


export default UserForm