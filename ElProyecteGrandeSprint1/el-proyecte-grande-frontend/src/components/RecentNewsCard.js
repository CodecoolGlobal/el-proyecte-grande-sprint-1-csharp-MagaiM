import React from 'react'

const RecentNewsCard = ({ news, formattedDate }) => {

  return (
    <div className="news-card">
      <img className="card-image" src={String(news.Image)} />
      <div className="card-body inner-card">
        <a className='card-title' href={String(news.Link)}><h5 className="card-title">{news.Title}</h5></a>
        <p className="card-text">{news.Description}</p>
        <p className="card-text">{formattedDate}</p>
      </div>
    </div>
  )
}

export default RecentNewsCard