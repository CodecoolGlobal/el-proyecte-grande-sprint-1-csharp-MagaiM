import React from 'react'

const OtherNewsCard = ({item, formattedDate}) => {
  return (
    <div className="col-lg-3 col-lg-3" style={{display: 'inline-block', width: '100%'}}>
        <div className="card" style={{flexDirection: 'row', backgroundColor: '#3A373F'}}>
            <img className="product-image" src={String(item.Image)} style={{maxWidth: '50%', maxHeight: '300px'}}/>
            <div className="card-body inner-card" style={{width: '50%'}}>
                <a href={String(item.Link)} style={{textDecoration: 'none', color: '#c9b18e'}}><h5 className="card-title">{item.Title}</h5></a>
                <p className="card-text">{item.Description}</p>
                <p className="card-text">{formattedDate}</p>
                <a href={String(item.Source.Url)} style={{textDecoration: 'none', color: '#c9b18e'}}><p className="card-text">{item.Source.Name}</p></a>
            </div>
        </div>
    </div>
  )
}

export default OtherNewsCard