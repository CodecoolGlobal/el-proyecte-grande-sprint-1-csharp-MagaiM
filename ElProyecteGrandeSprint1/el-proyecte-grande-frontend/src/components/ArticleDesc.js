import React from 'react'
import PropTypes from "prop-types";

const ArticleDesc = ({item, openArticle}) => {

  return (
    <div className="news-card">
        <div className="card-body inner-card">
            <a className='card-title' onClick={openArticle}><h5 className="card-title" data-article-id={item.id}>{item.title}</h5></a>
            <p className="card-text">{item.description}</p>
            <a className='publisher-link'><p className="card-text">{item.author.userName}</p></a>
        </div>
    </div>
  )
}

ArticleDesc.propTypes ={
  openArticle: PropTypes.func
};

export default ArticleDesc