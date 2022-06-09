import React from 'react'

const RecentNewsCard = ({news}) => {
  return (
    <div className="card" style={{flexDirection: 'row', backgroundColor: '#3A373F'}}>
        <img className="card-image" src={String(news.image)} style={{maxHeight: '300px',maxWidth: '50%'}}/>
        <div className="card-body inner-card" style={{width: '50%'}}>
            <a href={String(news.link)} style={{textDecoration: 'none', color: '#c9b18e'}}><h5 className="card-title">{news.title}</h5></a>
            <p className="card-text">{news.description}</p>
            <p className="card-text">{news.date}</p>
        </div>
    </div>
  )
}

export default RecentNewsCard