import React from 'react'

const RecentNewsCard = ({news}) => {
  
  return (
    <div className="news-card">
        <img className="card-image" src={String(news.image)}/>
        <div className="card-body inner-card">
            <a className='card-title' href={String(news.link)}><h5 className="card-title">{news.title}</h5></a>
            <p className="card-text">{news.description}</p>
            <p className="card-text">{news.date}</p>
        </div>
    </div>
  )
}

export default RecentNewsCard