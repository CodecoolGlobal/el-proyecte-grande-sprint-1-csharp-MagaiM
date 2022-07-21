import React, { useState, useRef } from "react";
import { useNavigate, Link } from 'react-router-dom';
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import AuthService from "../services/auth.service";

const required = (value) => {
  if (!value) {
    return (
      <div className="alert alert-danger" role="alert">
        This field is required!
      </div>
    );
  }
};

const Login = () => {
  let navigate = useNavigate();
  const form = useRef();
  const checkBtn = useRef();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState("");
  const onChangeUsername = (e) => {
    const username = e.target.value;
    setUsername(username);
  };

  const onChangePassword = (e) => {
    const password = e.target.value;
    setPassword(password);
  };

  const handleLogin = (e) => {
    e.preventDefault();
    setMessage("");
    setLoading(true);
    form.current.validateAll();
    if (checkBtn.current.context._errors.length === 0) {
      AuthService.login(username, password).then(
        (data) => {
          if (data !== 'false') {
            navigate("/");
            window.location.reload();
          }
          else{
            setLoading(false);
            const resMessage = 'Invalid User name or Password!'
            setMessage(resMessage);
          }
        }//,
        // (error) => {
        //   const resMessage =
        //   (error.response &&
        //     error.response.data &&
        //     error.response.data.message) ||
        //     error.message ||
        //     error.toString();
        //   setLoading(false);
        //   setMessage(resMessage);
        // }
      );
    } else {
      setLoading(false);
    }
  };

  return (
    <div className="col-md-12">
      <div className="card card-container">
        <img
          src="//ssl.gstatic.com/accounts/ui/avatar_2x.png"
          alt="profile-img"
          className="profile-img-card"
        />
        <Form className='form-style-1' onSubmit={handleLogin} ref={form}>
          <h3 style={{ textAlignLast: "center", padding: "1rem" }} >Login</h3>
          <div className="form-group">
            <label htmlFor="username">Username</label>
            <Input
              type="text"
              className="form-control form-field-input-box btn-outline-light"
              name="username"
              value={username}
              onChange={onChangeUsername}
              validations={[required]}
            />
          </div>
          <div className="form-group">
            <label htmlFor="password">Password</label>
            <Input
              type="password"
              className="form-control form-field-input-box btn-outline-light"
              name="password"
              value={password}
              onChange={onChangePassword}
              validations={[required]}
            />
          </div>
          <div className="form-group">
            <button className="btn btn-primary btn-block btn-dark login-btn" disabled={loading}>
              {loading && (
                <span className="spinner-border spinner-border-sm"></span>
              )}
              <span>Login</span>
            </button>
          </div>
          {message && (
            <div className="form-group">
              <div className="alert alert-danger" role="alert">
                {message}
              </div>
            </div>
          )}
          <CheckButton style={{ display: "none" }} ref={checkBtn} />
        </Form>
              <Link to={"/forgotPassword"} className="nav-link">
                Forgot your password?
              </Link>
      </div>
    </div>
    
  );
};

export default Login;

// import PropTypes from "prop-types";
// import UserForm from "../components/UserForm";
// import { useCookies } from 'react-cookie';

// const Login = ({postData, getData}) => {

  // let User;
  // const [cookies, setCookie] = useCookies(['user_id']);
  

  // const SendDataToBackEnd = async (inputs) => {
  //     let validateResponse = await postData("Validate", inputs);
  //     if (validateResponse === "True") {
  //         User = await getData(`name/${inputs["username"]}`)
  //         setCookie('user_id', User["id"], {path: '/'});
  //         localStorage.setItem('user_id', User["id"]);
  //         localStorage.setItem('user_name', User["userName"]);
  //         localStorage.setItem('user_reputation', User["reputation"]);
  //         localStorage.setItem('user_rank', User["rank"]);
  //     }
  //     return validateResponse;
  // }

//   return (
//     <UserForm postData={postData} SendDataToBackEnd={SendDataToBackEnd} Page={"Login"}></UserForm>
//   )
// }

// Login.prototype = {
//     postData: PropTypes.func,
//     getData: PropTypes.func
// }

// export default Login