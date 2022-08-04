import React from 'react'
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import AuthService from "../services/auth.service";

const Article = ({ item, closeArticle }) => {
    const currentUser = AuthService.getCurrentUser();
    return (
        <>
            <div>
                <button className="top-news-button btn-outline-light" type="button" onClick={closeArticle}>
                    <span className="top-news-button-content">Bak to Articles</span>
                </button>
            </div>
            <div className="news-card">
                <div className="card-body inner-card">
                    <a className='card-title'><h5 className="card-title">{item.title}</h5></a>
                    <p className="card-text">{item.description}</p>
                    <br />
                    <p className="card-text">{item.articleText}</p>
                    <a className='publisher-link'><p className="card-text">{item.author.userName}</p></a>
                </div>
            </div>
            {currentUser && item.author.id === currentUser.Id &&
                <Link className="nav-link" type='button' to={`/articles-editor/${item.id}`}>Edit</Link>}
        </>
    )
}

Article.propTypes = {
    closeArticle: PropTypes.func
};

export default Article