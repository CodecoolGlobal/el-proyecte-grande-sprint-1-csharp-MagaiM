import React from 'react'
import PropTypes from "prop-types";

const Article = ({item, closeArticle}) => {
    console.log(item);
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
                    <br/>
                    <p className="card-text">{item.articleText}</p>
                    <a className='publisher-link'><p className="card-text">{item.author.userName}</p></a>
                </div>
            </div>
        </>
    )
}

Article.propTypes ={
    closeArticle: PropTypes.func
};

export default Article