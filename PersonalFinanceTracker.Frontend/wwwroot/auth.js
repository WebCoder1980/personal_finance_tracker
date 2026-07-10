window.authStorage = {
    getToken: () => localStorage.getItem('jwt'),
    setToken: (token) => localStorage.setItem('jwt', token),
    removeToken: () => localStorage.removeItem('jwt')
};
