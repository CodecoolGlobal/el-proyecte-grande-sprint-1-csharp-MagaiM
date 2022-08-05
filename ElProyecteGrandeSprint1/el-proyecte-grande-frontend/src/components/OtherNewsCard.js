import React from 'react'

const OtherNewsCard = ({ item, formattedDate }) => {

  return (
    <div className="news-card">
      <img className="card-image" src={String(item.Image)} />
      <div className="card-body inner-card">
        <a className='card-title' href={String(item.Link)}><h5 className="card-title">{item.Title}</h5></a>
        <p className="card-text">{item.Description}</p>
        <p className="card-text">{formattedDate}</p>
        <a className='publisher-link' href={String(item.Source.Url)}><p className="card-text">{item.Source.Name}</p></a>
      </div>
    </div>
  )
}

export default OtherNewsCard