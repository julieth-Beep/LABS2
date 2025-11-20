<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="PruebaLABS.Vista.Inicio" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>LABS - Soluciones de Logística y Transporte</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />
</head>
<body>
    <form id="form1" runat="server">

        <nav class="navbar navbar-expand-lg bg-light sticky-top shadow">
            <div class="container">
                <a href="Inicio.aspx" class="navbar-brand fw-bold fs-3 text-success">
                    <i class="bi bi-truck-front-fill me-2"></i>LABS
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarMenu">
                    <span class="navbar-toggler-icon"></span>
                </button>

                <div class="collapse navbar-collapse" id="navbarMenu">
                    <ul class="navbar-nav ms-auto mb-2 mb-lg-0 fw-semibold">
                        <li class="nav-item"><a class="nav-link text-success active" href="#inicio">Inicio</a></li>
                        <li class="nav-item"><a class="nav-link text-success" href="#about">Nosotros</a></li>
                        <li class="nav-item"><a class="nav-link text-success" href="#services">Servicios</a></li>
                        <li class="nav-item"><a class="nav-link text-success" href="#process">Procesos</a></li>
                        <li class="nav-item"><a class="nav-link text-success" href="#contact">Contacto</a></li>
                        <li class="nav-item"><a class="nav-link text-success" href="#trabaja">Trabaja con Nosotros</a></li>

                        <li id="liLogin" runat="server" class="nav-item">
                            <a class="btn btn-outline-success ms-lg-3" href="Login.aspx">Iniciar sesión</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <section id="inicio" class="position-relative text-white d-flex align-items-center justify-content-center text-center"
            style="min-height: 70vh; background: url('https://wp.tyt.com.mx/wp-content/uploads/2016/11/kenworth-gpo-ed-1-1024x597.jpg') center/cover no-repeat;">
            <div class="position-absolute top-0 start-0 w-100 h-100" style="background-color: rgba(0,0,0,0.5);"></div>
            <div class="container position-relative">
                <h1 class="display-4 fw-bold">LABS</h1>
                <p class="lead my-3">Soluciones de logística y transporte confiable con seguimiento en tiempo real</p>
                <a href="#contact" class="btn btn-success btn-lg">Solicita una Cotización</a>
            </div>
        </section>

        <section id="about" class="py-5">
            <div class="container text-center">
                <h2 class="fw-bold mb-5 text-success">Sobre Nosotros</h2>
                <div class="row">
                    <div class="col-lg-4 mb-4">
                        <div class="p-4 shadow-sm rounded bg-light">
                            <i class="fas fa-truck fa-2x text-success mb-3"></i>
                            <h3>Nuestra Pasión</h3>
                            <p>Brindamos soluciones logísticas con puntualidad, seguridad y eficiencia, siempre a la vanguardia tecnológica.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 mb-4">
                        <div class="p-4 shadow-sm rounded bg-light">
                            <i class="fas fa-user-tie fa-2x text-success mb-3"></i>
                            <h3>Expertos en Transporte</h3>
                            <p>Contamos con profesionales en logística y conductores altamente capacitados para cada servicio.</p>
                        </div>
                    </div>
                    <div class="col-lg-4 mb-4">
                        <div class="p-4 shadow-sm rounded bg-light">
                            <i class="fas fa-check fa-2x text-success mb-3"></i>
                            <h3>Valores</h3>
                            <p>Responsabilidad, compromiso, innovación y confianza en cada envío que realizamos.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section id="services" class="py-5 bg-light text-center">
            <div class="container">
                <h2 class="fw-bold mb-4 text-success">Nuestros Servicios</h2>
                <div class="row">
                    <div class="col-md-4">
                        <i class="fas fa-truck fa-3x mb-3 text-success"></i>
                        <h4>Transporte de carga</h4>
                        <p>Movemos tu carga con seguridad y eficiencia.</p>
                    </div>
                    <div class="col-md-4">
                        <i class="fas fa-shipping-fast fa-3x mb-3 text-success"></i>
                        <h4>Envíos rápidos</h4>
                        <p>Entregas ágiles y puntuales a nivel nacional.</p>
                    </div>
                    <div class="col-md-4">
                        <i class="fas fa-box fa-3x mb-3 text-success"></i>
                        <h4>Gestión de inventarios</h4>
                        <p>Control y almacenamiento de mercancías con precisión.</p>
                    </div>
                </div>
            </div>
        </section>

        <section id="process" class="py-5 text-center">
            <div class="container">
                <h2 class="fw-bold mb-4 text-success">Nuestro Proceso</h2>
                <div class="row">
                    <div class="col-md-4">
                        <i class="fas fa-phone fa-3x mb-3 text-success"></i>
                        <h4>1. Solicitud de transporte</h4>
                        <p>Registra los datos de tu envío en nuestra plataforma.</p>
                    </div>
                    <div class="col-md-4">
                        <i class="fas fa-truck-arrow-right fa-3x mb-3 text-success"></i>
                        <h4>2. Asignación de vehículo</h4>
                        <p>Asignamos el vehículo y conductor óptimos automáticamente.</p>
                    </div>
                    <div class="col-md-4">
                        <i class="fa-regular fa-address-book fa-3x mb-3 text-success"></i>
                        <h4>3. Seguimiento en tiempo real</h4>
                        <p>Monitoreo continuo del envío durante todo el trayecto.</p>
                    </div>
                </div>
            </div>
        </section>

        <section id="contact" class="py-5">
            <div class="container">
                <h2 class="fw-bold text-success mb-5 text-center">Contáctanos</h2>
                <div class="row">
                    <div class="col-lg-6 mb-4">
                        <div class="card shadow-sm">
                            <div class="card-body">
                                <h4 class="card-title mb-4">Información de Contacto</h4>
                                <p><i class="fas fa-map-marker-alt me-2 text-success"></i>Calle 123, Sogamoso, Boyacá</p>
                                <p><i class="fas fa-phone me-2 text-success"></i>+57 300 123 4567</p>
                                <p><i class="fas fa-envelope me-2 text-success"></i>contacto@labs-logistica.com</p>
                                <p><i class="fas fa-clock me-2 text-success"></i>Lunes a Sábado 8:00 am - 6:00 pm</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-6 mb-4">
                        <div class="card shadow-sm">
                            <div class="card-body">
                                <h4 class="card-title mb-4">Solicita tu servicio</h4>
                                <div class="mb-3">
                                    <label class="form-label">Nombre completo</label>
                                    <input type="text" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Correo electrónico</label>
                                    <input type="email" class="form-control" required />
                                </div>
                                <div class="mb-3">
                                    <label class="form-label">Detalle del servicio</label>
                                    <textarea class="form-control" rows="3" required></textarea>
                                </div>
                                <button type="submit" class="btn btn-success">Enviar solicitud</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <section id="trabaja" class="position-relative text-white text-center" style="height: 80vh; overflow: hidden;">
            <img src="https://www.lctransportes.com/wp-content/uploads/2021/01/graneles2.jpg"
                class="img-fluid w-100 h-100 object-fit-cover" style="opacity: 0.35;" alt="Camión de carga">
            <div class="position-absolute top-0 start-0 w-100 h-100 bg-dark" style="opacity: 0.5;"></div>
            <div class="position-absolute top-50 start-50 translate-middle w-100 px-3">
                <h2 class="fw-bold mb-3">Trabaja con Nosotros</h2>
                <p class="lead mb-4">
                    Si eres propietario de una tractomula, en <strong>LABS</strong> te conectamos con cargas en todo el país.
                    Únete a nuestra red logística y comienza a mover el progreso de Colombia.
                </p>
                <div class="container">
                    <div class="row justify-content-center">
                        <div class="col-md-6">
                            <input type="email" class="form-control form-control-lg mb-3" placeholder="Correo electrónico">
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-md-3">
                            <button class="btn btn-success btn-lg w-100 fw-bold">REGISTRARSE</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <footer class="bg-white text-center py-4">
            <div class="container">
                <h4 class="fw-bold mb-3 text-success">Síguenos en nuestras redes</h4>
                <div class="mb-3">
                    <a href="#" class="text-success me-3"><i class="bi bi-facebook fs-4"></i></a>
                    <a href="#" class="text-success me-3"><i class="bi bi-instagram fs-4"></i></a>
                    <a href="#" class="text-success me-3"><i class="bi bi-twitter-x fs-4"></i></a>
                    <a href="#" class="text-success me-3"><i class="bi bi-linkedin fs-4"></i></a>
                    <a href="#" class="text-success"><i class="bi bi-youtube fs-4"></i></a>
                </div>
                <hr class="border-success" />
                <p class="mb-0 small text-success fw-semibold">
                     <span class="fw-bold">LABS Logística</span> — Moviendo el progreso de Colombia
                </p>
            </div>
        </footer>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
