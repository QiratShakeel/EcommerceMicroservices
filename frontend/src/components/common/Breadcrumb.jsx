import { useLocation, Link } from "react-router-dom"

export const Breadcrumb = () => {
    const location = useLocation();

    const pathnames = location.pathname.split("/").filter(x => x);
    if (pathnames.length === 0) return null;
    return (
        <div id="breadcrumb" className="section">
            {/* <!-- container --> */}
            <div className="container">
                {/* <!-- row --> */}
                <div className="row">
                    <div className="col-md-12">
                        {/* <h3 className="breadcrumb-header">Checkout</h3> */}
                        <ul className="breadcrumb-tree">
                            <li><Link to="/">Home</Link></li>
                            {pathnames.map((name, index) => {
                                const routeTo = `/${pathnames.slice(0, index + 1).join("/")}`;
                                return (
                                    //lenght total items in list or index starts from 0 so for index we did -1 
                                    <li className={index === pathnames.length - 1 ? "active" : ""} key={index}><Link to={routeTo}>{name}</Link></li>
                                )
                            })}
                        </ul>
                    </div>
                </div>
                {/* <!-- /row --> */}
            </div>
            {/* <!-- /container --> */}
        </div>
    )
}

// ------------React Basic Learning Path
///JS basics:
// array
// map
// filter
// objects
// 2. React basics:
// components
// props
// state
// hooks (useState, useEffect)
// 3. Routing:
// useLocation
// Link
// routes
// 4. Real project:
// ecommerce UI
// CRUD app