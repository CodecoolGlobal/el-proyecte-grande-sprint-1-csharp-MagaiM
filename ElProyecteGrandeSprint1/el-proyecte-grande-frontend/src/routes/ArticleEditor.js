import React, { useState, useEffect, useRef } from "react";
import { useParams } from 'react-router-dom';
import Form from "react-validation/build/form";
import Input from "react-validation/build/input";
import CheckButton from "react-validation/build/button";
import UserService from "../services/user.service";
import AuthService from "../services/auth.service";



const ArticleEditor = () => {
  let { Id } = useParams();
  const form = useRef();
  const checkBtn = useRef();
  const [content, setContent] = useState([]);
  const [currentArticle, setCurrentArticle] = useState([]);
  const [loading, setLoading] = useState(false);
  const [message, setMessage] = useState("");
  const [Title, setTitle] = useState("");
  const [Desc, setDesc] = useState("");
  const [Theme, setTheme] = useState("");
  const [Text, setText] = useState("");
  const currentUser = AuthService.getCurrentUser();

  if (currentUser === null)
    window.location.href = '/login';

  useEffect(() => {
    UserService.getArticleBoard().then(
      (response) => {
        setContent(response.data);
      },
      (error) => {
        const _content =
          (error.response &&
            error.response.data &&
            error.response.data.message) ||
          error.message ||
          error.toString();
        setContent(_content);
      }
    );
  }, []);


  useEffect(() => {
    openArticle();
  }, [content]);


  useEffect(() => {
    setTitle(currentArticle.title);
    setDesc(currentArticle.description);
    setTheme(currentArticle.theme);
    setText(currentArticle.articleText);
  }, [currentArticle]);

  const onChangeTitle = (e) => {
    const Title = e.target.value;
    setTitle(Title);
  };

  const onChangeDesc = (e) => {
    const Desc = e.target.value;
    setDesc(Desc);
  };

  const onChangeTheme = (e) => {
    const Theme = e.target.value;
    setTheme(Theme);
  };

  const onChangeText = (e) => {
    const Text = e.target.value;
    setText(Text);
  };


  const onSubmit = (e) => {
    e.preventDefault();
    setMessage("");
    setLoading(true);
    form.current.validateAll();
    if (checkBtn.current.context._errors.length === 0) {
      UserService.ChangeArticle(Title, Desc, Theme, Text, Id, currentArticle.author.userName).then(
        (data) => {
          if (data.data === "True") {
            window.location.href = '/articles';
          }
        },
      );
    } else {
      setLoading(false);
    }

  };

  const openArticle = () => {
    console.log(content)
    content.forEach(element => {
      if (element.id == Id) {
        setCurrentArticle(element);
      }
    });
  };

  return (
    <div className="col-md-12">
      <div className="card form-card">
        <Form onSubmit={onSubmit} ref={form}>
          <div className="form-group">
            <label htmlFor="Title">Edit title</label>
            <Input
              type="text"
              className="form-control"
              name="Title"
              value={Title}
              onChange={onChangeTitle}
            />
          </div>
          <div className="form-group">
            <label htmlFor="Description">Edit description</label>
            <Input
              type="text"
              className="form-control"
              name="Description"
              value={Desc}
              onChange={onChangeDesc}
            />
          </div>
          <div className="form-group">
            <label htmlFor="Theme">Edit theme</label>
            <Input
              type="text"
              className="form-control"
              name="Theme"
              value={Theme}
              onChange={onChangeTheme}
            />
          </div>
          <div className="form-group">
            <label htmlFor="ArticleText">Edit text</label>
            <textarea
              rows={10}
              type="text"
              className="form-control"
              name="ArticleText"
              value={Text}
              onChange={onChangeText}
            />
          </div>
          <div className="form-group">
            <button className="btn btn-primary btn-block btn-dark login-btn" disabled={loading}>
              {loading && (
                <span className="spinner-border spinner-border-sm"></span>
              )}
              <span>Edit article</span>
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
  )
}

export default ArticleEditor