import React, { useState, useRef } from "react";
import { useNavigate, useParams } from 'react-router-dom';
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import AuthService from "../services/auth.service";
import CheckButton from "react-validation/build/button";

const required = (value) => {
  if (!value) {
    return (
      <div className="alert alert-danger" role="alert">
        This field is required!
      </div>
    );
  }
};

const vpassword = (value) => {
  if (value.length < 6 || value.length > 40) {
    return (
      <div className="alert alert-danger" role="alert">
        The password must be between 6 and 40 characters.
      </div>
    );
  }
};



const NewPassword = () => {
  let { emailId } = useParams();
  let navigate = useNavigate();
  const form = useRef();
  const checkBtn = useRef();
  const [loading, setLoading] = useState(false);
  const [password, setPassword] = useState("");
  const [message, setMessage] = useState("");

  const onChangePassword = (e) => {
    const password = e.target.value;
    setPassword(password);
  };


  const onSubmit = (e) => {
    e.preventDefault();
    setMessage("");
    setLoading(true);
    form.current.validateAll();
    if (checkBtn.current.context._errors.length === 0) {
      AuthService.ChangePassword(password, "", "", emailId).then(
        (data) => {
          if (data === "Your profile was Changed successfully") {
            navigate("/login");
            window.location.reload();
          } else if (data === "No") {
            const resMessage = "Id expired"
            setLoading(false);
            setMessage(resMessage);
          } else {
            const resMessage = "Invalid password"
            setLoading(false);
            setMessage(resMessage);
          }
        },
        // (error) => {
        //   const resMessage =
        //     (error.response &&
        //       error.response.data &&
        //       error.response.data.message) ||
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
        <Form onSubmit={onSubmit} ref={form}>
          <div className="form-group">
            <label htmlFor="password">New Password</label>
            <Input
              type="password"
              className="form-control"
              name="password"
              value={password}
              onChange={onChangePassword}
              validations={[required, vpassword]}
            />
          </div>
          <div className="form-group">
            <button className="btn btn-primary btn-block btn-dark login-btn" disabled={loading}>
              {loading && (
                <span className="spinner-border spinner-border-sm"></span>
              )}
              <span>Change Password</span>
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
      </div>
    </div>

  );
};

export default NewPassword
