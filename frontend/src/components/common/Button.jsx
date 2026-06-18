import { Link } from "react-router-dom";

export const Button = ({text, to, onClick, variant = "primary"}) =>{
    const styles = {
    primary: "from-cyan-400 via-cyan-500 to-cyan-600 focus:ring-cyan-300 dark:focus:ring-cyan-800",
    secondary: "from-lime-200 via-lime-400 to-lime-500 focus:ring-lime-300 dark:focus:ring-lime-800 ",
    danger: "from-red-400 via-red-500 to-red-600 focus:ring-red-300 dark:focus:ring-red-800"
    };
    if (to) {
    return (
      <Link to={to} className={`text-white bg-gradient-to-r hover:bg-gradient-to-br focus:ring-4 focus:outline-none font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2 ${styles[variant]}`}>
        {text}
      </Link>
    );
    }
    return(        
        <button type="button" onClick={onClick} className={`text-white bg-gradient-to-r hover:bg-gradient-to-br focus:ring-4 focus:outline-none font-medium rounded-lg text-sm px-5 py-2.5 text-center me-2 mb-2 ${styles[variant]}`}> {text}</button>

    )
}
