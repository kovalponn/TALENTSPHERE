// TalentSphere - Interactive JavaScript

// Skeleton Loading Helper
function showSkeletonLoader(container, count = 3) {
    const skeletonHTML = `
        <div class="skeleton-card skeleton" style="margin-bottom: 16px;">
            <div class="skeleton-title skeleton"></div>
            <div class="skeleton-text skeleton"></div>
            <div class="skeleton-text skeleton" style="width: 80%;"></div>
        </div>
    `;
    if (container) {
        container.innerHTML = skeletonHTML.repeat(count);
    }
}

function hideSkeletonLoader(container, content) {
    if (container) {
        setTimeout(() => {
            container.innerHTML = content;
            container.classList.add('fade-in');
        }, 500);
    }
}

// Enhanced Micro-interactions
function addRippleEffect(button, event) {
    const ripple = document.createElement('span');
    const rect = button.getBoundingClientRect();
    const size = Math.max(rect.width, rect.height);
    const x = event.clientX - rect.left - size / 2;
    const y = event.clientY - rect.top - size / 2;

    ripple.style.width = ripple.style.height = size + 'px';
    ripple.style.left = x + 'px';
    ripple.style.top = y + 'px';
    ripple.classList.add('ripple');

    button.appendChild(ripple);
    setTimeout(() => ripple.remove(), 600);
}

document.addEventListener('DOMContentLoaded', () => {
    // Mobile Menu Toggle
    const mobileMenuToggle = document.getElementById('mobileMenuToggle');
    const mobileMenu = document.getElementById('mobileMenu');

    if (mobileMenuToggle && mobileMenu) {
        mobileMenuToggle.addEventListener('click', () => {
            mobileMenuToggle.classList.toggle('active');
            mobileMenu.classList.toggle('active');
            document.body.style.overflow = mobileMenu.classList.contains('active') ? 'hidden' : '';
        });

        // Close menu when clicking on links
        mobileMenu.querySelectorAll('a').forEach(link => {
            link.addEventListener('click', () => {
                mobileMenuToggle.classList.remove('active');
                mobileMenu.classList.remove('active');
                document.body.style.overflow = '';
            });
        });

        // Close menu on window resize
        window.addEventListener('resize', () => {
            if (window.innerWidth > 768) {
                mobileMenuToggle.classList.remove('active');
                mobileMenu.classList.remove('active');
                document.body.style.overflow = '';
            }
        });
    }

    // Smooth scroll for anchor links
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                target.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });

    // Add hover effects to cards
    const cards = document.querySelectorAll('.glass-card, .marketplace-card, .project-card');
    cards.forEach(card => {
        card.addEventListener('mouseenter', function () {
            this.style.transform = 'translateY(-4px)';
        });
        card.addEventListener('mouseleave', function () {
            this.style.transform = 'translateY(0)';
        });
    });

    // Animate stats on scroll
    const animateStats = () => {
        const statValues = document.querySelectorAll('.stat-value, .stat-value-profile');

        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const target = entry.target;
                    const finalValue = target.textContent;

                    // Simple animation for demonstration
                    target.style.opacity = '0';
                    setTimeout(() => {
                        target.style.transition = 'opacity 0.5s ease';
                        target.style.opacity = '1';
                    }, 100);

                    observer.unobserve(target);
                }
            });
        }, { threshold: 0.5 });

        statValues.forEach(stat => observer.observe(stat));
    };

    animateStats();

    // 3D Sphere rotation animation
    const sphere = document.querySelector('.sphere-3d');
    if (sphere) {
        let rotation = 0;
        setInterval(() => {
            rotation += 0.5;
            sphere.style.transform = `rotate(${rotation}deg)`;
        }, 50);
    }

    // Progress bars animation
    const animateProgressBars = () => {
        const progressBars = document.querySelectorAll('.progress-fill, .skill-progress');

        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    const bar = entry.target;
                    const width = bar.style.width;
                    bar.style.width = '0';
                    setTimeout(() => {
                        bar.style.transition = 'width 1s ease';
                        bar.style.width = width;
                    }, 200);
                    observer.unobserve(bar);
                }
            });
        }, { threshold: 0.5 });

        progressBars.forEach(bar => observer.observe(bar));
    };

    animateProgressBars();

    // Tab functionality
    const tabs = document.querySelectorAll('.tab-btn');
    tabs.forEach(tab => {
        tab.addEventListener('click', function () {
            // Remove active class from all tabs
            tabs.forEach(t => t.classList.remove('active'));
            // Add active class to clicked tab
            this.classList.add('active');

            // Handle content switching
            const targetSelector = this.getAttribute('data-tab');
            if (targetSelector) {
                const allContents = document.querySelectorAll('.tab-content');
                allContents.forEach(content => content.classList.remove('active'));

                const targetContent = document.querySelector(`.tab-content[data-content="${targetSelector}"]`);
                if (targetContent) {
                    targetContent.classList.add('active');
                }
            }

            // Add smooth transition effect
            this.style.transform = 'scale(1.05)';
            setTimeout(() => {
                this.style.transform = 'scale(1)';
            }, 200);
        });
    });

    // Filter checkboxes
    const filterCheckboxes = document.querySelectorAll('.filter-option input');
    filterCheckboxes.forEach(checkbox => {
        checkbox.addEventListener('change', function () {
            const label = this.closest('.filter-option');
            if (this.checked) {
                label.style.backgroundColor = 'rgba(255, 255, 255, 0.05)';
            } else {
                label.style.backgroundColor = 'transparent';
            }
        });
    });

    // Chat functionality
    const messageInput = document.querySelector('.message-input');
    const sendBtn = document.querySelector('.send-btn');
    const messagesArea = document.querySelector('.messages-area');

    if (sendBtn && messageInput) {
        const sendMessage = () => {
            const message = messageInput.value.trim();
            if (message) {
                // Create new message element
                const messageDiv = document.createElement('div');
                messageDiv.className = 'message outgoing';
                messageDiv.innerHTML = `
                    <div class="message-content">
                        <div class="message-bubble glass-card">
                            <p>${message}</p>
                        </div>
                        <span class="message-time">${new Date().toLocaleTimeString('ru-RU', { hour: '2-digit', minute: '2-digit' })}</span>
                    </div>
                `;

                // Remove typing indicator if exists
                const typingIndicator = document.querySelector('.typing-indicator');
                if (typingIndicator) {
                    typingIndicator.remove();
                }

                // Add message to chat
                if (messagesArea) {
                    messagesArea.appendChild(messageDiv);
                    messagesArea.scrollTop = messagesArea.scrollHeight;
                }

                // Clear input
                messageInput.value = '';

                // Simulate typing indicator after 2 seconds
                setTimeout(() => {
                    const typingDiv = document.createElement('div');
                    typingDiv.className = 'typing-indicator';
                    typingDiv.innerHTML = `
                        <div class="typing-avatar neon-glow-blue"></div>
                        <div class="typing-bubble glass-card">
                            <div class="typing-dots">
                                <span></span>
                                <span></span>
                                <span></span>
                            </div>
                        </div>
                    `;
                    if (messagesArea) {
                        messagesArea.appendChild(typingDiv);
                        messagesArea.scrollTop = messagesArea.scrollHeight;
                    }
                }, 2000);
            }
        };

        sendBtn.addEventListener('click', sendMessage);
        messageInput.addEventListener('keypress', (e) => {
            if (e.key === 'Enter' && !e.shiftKey) {
                e.preventDefault();
                sendMessage();
            }
        });
    }

    // Chat list item click
    const chatItems = document.querySelectorAll('.chat-item');
    chatItems.forEach(item => {
        item.addEventListener('click', function () {
            chatItems.forEach(i => i.classList.remove('active'));
            this.classList.add('active');

            // Remove unread badge
            const badge = this.querySelector('.unread-badge');
            if (badge) {
                badge.style.opacity = '0';
                setTimeout(() => badge.remove(), 300);
            }
        });
    });

    // Apply button hover effects
    const applyButtons = document.querySelectorAll('.apply-btn, .neon-button');
    applyButtons.forEach(btn => {
        btn.addEventListener('mouseenter', function () {
            this.style.transform = 'translateY(-2px) scale(1.02)';
        });
        btn.addEventListener('mouseleave', function () {
            this.style.transform = 'translateY(0) scale(1)';
        });
    });

    // Portfolio item hover
    const portfolioItems = document.querySelectorAll('.portfolio-item');
    portfolioItems.forEach(item => {
        item.addEventListener('mouseenter', function () {
            const overlay = this.querySelector('.thumbnail-overlay');
            if (overlay) {
                overlay.style.opacity = '1';
            }
        });
        item.addEventListener('mouseleave', function () {
            const overlay = this.querySelector('.thumbnail-overlay');
            if (overlay) {
                overlay.style.opacity = '0';
            }
        });
    });

    // Search functionality
    const searchInputs = document.querySelectorAll('input[type="text"][placeholder*="Поиск"]');
    searchInputs.forEach(input => {
        input.addEventListener('focus', function () {
            this.parentElement.style.borderColor = 'rgba(176, 0, 255, 0.5)';
            this.parentElement.style.boxShadow = '0 0 20px rgba(176, 0, 255, 0.2)';
        });
        input.addEventListener('blur', function () {
            this.parentElement.style.borderColor = 'rgba(255, 255, 255, 0.1)';
            this.parentElement.style.boxShadow = 'none';
        });
    });

    // Notification animations
    const badges = document.querySelectorAll('.badge');
    badges.forEach(badge => {
        setInterval(() => {
            badge.style.transform = 'scale(1.1)';
            setTimeout(() => {
                badge.style.transform = 'scale(1)';
            }, 200);
        }, 3000);
    });

    // Add parallax effect to hero section
    const hero = document.querySelector('.hero');
    if (hero) {
        window.addEventListener('scroll', () => {
            const scrolled = window.pageYOffset;
            const heroVisual = document.querySelector('.hero-visual');
            if (heroVisual) {
                heroVisual.style.transform = `translateY(${scrolled * 0.3}px)`;
            }
        });
    }

    // Animate elements on scroll
    const animateOnScroll = () => {
        const elements = document.querySelectorAll('.feature-card, .marketplace-card, .stat-card');

        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    entry.target.style.opacity = '0';
                    entry.target.style.transform = 'translateY(30px)';

                    setTimeout(() => {
                        entry.target.style.transition = 'all 0.6s ease';
                        entry.target.style.opacity = '1';
                        entry.target.style.transform = 'translateY(0)';
                    }, 100);

                    observer.unobserve(entry.target);
                }
            });
        }, { threshold: 0.1 });

        elements.forEach(el => observer.observe(el));
    };

    animateOnScroll();

    // Loading animation for page transitions
    window.addEventListener('beforeunload', () => {
        document.body.style.opacity = '0.7';
    });

    // Mobile Messages - Contact List / Chat Toggle
    const mobileChatItems = document.querySelectorAll('.chat-item');
    const chatWindow = document.querySelector('.chat-window');
    const messagesContainer = document.querySelector('.messages-container');
    const backToContactsBtn = document.getElementById('backToContacts');

    if (mobileChatItems.length > 0 && chatWindow && window.innerWidth <= 768) {
        // При клике на контакт - открываем чат
        mobileChatItems.forEach(item => {
            item.addEventListener('click', () => {
                chatWindow.classList.add('active');
                if (messagesContainer) {
                    messagesContainer.classList.add('chat-active');
                }
            });
        });

        // Кнопка "Назад" - возвращаемся к списку
        if (backToContactsBtn) {
            backToContactsBtn.addEventListener('click', (e) => {
                e.stopPropagation();
                chatWindow.classList.remove('active');
                if (messagesContainer) {
                    messagesContainer.classList.remove('chat-active');
                }
            });
        }
    }

    // Dashboard/Pages Mobile Sidebar Toggle
    const mobileSidebarToggle = document.getElementById('mobileSidebarToggle');
    const dashboardSidebar = document.getElementById('dashboardSidebar');
    const dashboardOverlay = document.getElementById('sidebarOverlay');

    if (mobileSidebarToggle && dashboardSidebar && dashboardOverlay) {
        mobileSidebarToggle.addEventListener('click', () => {
            dashboardSidebar.classList.toggle('active');
            dashboardOverlay.classList.toggle('active');
        });

        dashboardOverlay.addEventListener('click', () => {
            dashboardSidebar.classList.remove('active');
            dashboardOverlay.classList.remove('active');
        });

        // Close on nav item click
        dashboardSidebar.querySelectorAll('.nav-item').forEach(item => {
            item.addEventListener('click', () => {
                dashboardSidebar.classList.remove('active');
                dashboardOverlay.classList.remove('active');
            });
        });
    }

    // Sidebar toggle for messages page (separate from main mobile menu)
    const sidebarToggle = document.getElementById('mobileMenuToggle');
    const sidebar = document.getElementById('sidebar');
    const sidebarOverlay = document.getElementById('sidebarOverlay');

    if (sidebarToggle && sidebar && sidebarOverlay) {
        // Toggle sidebar
        sidebarToggle.addEventListener('click', () => {
            sidebar.classList.toggle('active');
            sidebarOverlay.classList.toggle('active');
        });

        // Close sidebar when clicking overlay
        sidebarOverlay.addEventListener('click', () => {
            sidebar.classList.remove('active');
            sidebarOverlay.classList.remove('active');
        });

        // Close sidebar when clicking nav item
        const navItems = sidebar.querySelectorAll('.nav-item');
        navItems.forEach(item => {
            item.addEventListener('click', () => {
                sidebar.classList.remove('active');
                sidebarOverlay.classList.remove('active');
            });
        });
    }

    // Console welcome message
    console.log('%c🚀 TalentSphere Platform', 'color: #b000ff; font-size: 24px; font-weight: bold;');
    console.log('%cНео-Brutalism + Glassmorphism + Cyber Neon Design', 'color: #00d4ff; font-size: 14px;');
    console.log('%cФриланс платформа будущего', 'color: #ff0080; font-size: 12px;');
});

// Smooth page load animation
window.addEventListener('load', () => {
    document.body.style.opacity = '0';
    document.body.style.transition = 'opacity 0.5s ease';
    setTimeout(() => {
        document.body.style.opacity = '1';
    }, 100);
});

// Add glow effect on hover for neon elements
document.addEventListener('mousemove', (e) => {
    const neonElements = document.querySelectorAll('[class*="neon-glow"]');
    neonElements.forEach(el => {
        const rect = el.getBoundingClientRect();
        const x = e.clientX - rect.left;
        const y = e.clientY - rect.top;

        if (x >= 0 && x <= rect.width && y >= 0 && y <= rect.height) {
            const intensity = 1 - (Math.abs(x - rect.width / 2) / rect.width + Math.abs(y - rect.height / 2) / rect.height) / 2;
            el.style.filter = `brightness(${1 + intensity * 0.3})`;
        } else {
            el.style.filter = 'brightness(1)';
        }
    });
});

// Interactive Video Gallery Logic
document.addEventListener('DOMContentLoaded', () => {
    const videoModal = document.getElementById('videoModal');
    const closeModal = document.getElementById('closeModal');
    const videoPlayer = videoModal ? videoModal.querySelector('video') : null;

    // Main Preview Elements
    const mainPreview = document.querySelector('.video-placeholder');
    const mainImage = document.querySelector('.main-thumb');
    const mainTitle = document.querySelector('.video-title');
    const mainAuthor = document.querySelector('.video-author');

    // Thumbnails
    const thumbnails = document.querySelectorAll('.video-thumbnail-item');

    // 1. Hover Interaction (Switch Main Video)
    if (thumbnails.length > 0 && mainPreview) {
        thumbnails.forEach(thumb => {
            thumb.addEventListener('mouseenter', () => {
                // Update active state
                thumbnails.forEach(t => t.classList.remove('active'));
                thumb.classList.add('active');

                // Get data from hovered thumbnail
                const videoSrc = thumb.getAttribute('data-video-src');
                const imageSrc = thumb.getAttribute('data-image');
                const title = thumb.getAttribute('data-title');
                const author = thumb.getAttribute('data-author');

                // Update Main Preview
                if (mainPreview) mainPreview.setAttribute('data-video-src', videoSrc);
                if (mainImage) mainImage.src = imageSrc;
                if (mainTitle) mainTitle.textContent = title;
                if (mainAuthor) mainAuthor.textContent = author;
            });
        });
    }

    // 2. Modal Logic (Click Main Video)
    if (videoModal && videoPlayer && mainPreview) {
        // Open Modal
        mainPreview.addEventListener('click', () => {
            const videoSrc = mainPreview.getAttribute('data-video-src');
            if (videoSrc) {
                videoPlayer.src = videoSrc;
                videoModal.classList.add('active');
                videoPlayer.play();
                document.body.style.overflow = 'hidden'; // Prevent scrolling
            }
        });

        // Close Modal Function
        const closeVideoModal = () => {
            videoModal.classList.remove('active');
            videoPlayer.pause();
            videoPlayer.currentTime = 0;
            videoPlayer.src = ''; // Clear source
            document.body.style.overflow = ''; // Restore scrolling
        };

        // Close on button click
        if (closeModal) closeModal.addEventListener('click', closeVideoModal);

        // Close on click outside
        videoModal.addEventListener('click', (e) => {
            if (e.target === videoModal) {
                closeVideoModal();
            }
        });

        // Close on Escape key
        document.addEventListener('keydown', (e) => {
            if (e.key === 'Escape' && videoModal.classList.contains('active')) {
                closeVideoModal();
            }
        });
    }
});

// Mobile Course Navigation Logic
document.addEventListener('DOMContentLoaded', () => {
    const courseNavToggle = document.getElementById('courseNavToggle');
    const courseNav = document.getElementById('courseNav');
    const courseNavOverlay = document.getElementById('courseNavOverlay');
    const courseNavClose = document.getElementById('courseNavClose');

    if (courseNavToggle && courseNav && courseNavOverlay) {
        const openNav = () => {
            courseNav.classList.add('active');
            courseNavOverlay.classList.add('active');
            document.body.style.overflow = 'hidden';
        };

        const closeNav = () => {
            courseNav.classList.remove('active');
            courseNavOverlay.classList.remove('active');
            document.body.style.overflow = '';
        };

        courseNavToggle.addEventListener('click', openNav);
        if (courseNavClose) courseNavClose.addEventListener('click', closeNav);
        courseNavOverlay.addEventListener('click', closeNav);
    }
});

// Marketplace Filtering Logic
// Marketplace Filtering Logic
document.addEventListener('DOMContentLoaded', () => {
    // Only run on marketplace page
    if (!document.querySelector('.marketplace-page')) return;

    // --- Mock Data ---
    const mockProjects = [
        {
            id: 1,
            title: "Редизайн Dashboard для SaaS платформы",
            description: "Требуется создать современный интерфейс для B2B SaaS платформы с акцентом на аналитику и визуализацию данных. Необходимо проработать UX для сложных таблиц и графиков.",
            fullDescription: "Мы ищем опытного UI/UX дизайнера для полного редизайна нашей SaaS платформы. Текущий интерфейс устарел и не отвечает современным требованиям удобства. \n\n**Задачи:**\n- Аудит текущего интерфейса\n- Разработка новой дизайн-системы\n- Прототипирование основных сценариев\n- Дизайн макетов в Figma\n\n**Требования:**\n- Опыт в B2B SaaS от 3 лет\n- Портфолио с дашбордами\n- Знание Figma на продвинутом уровне",
            budget: 3500,
            deadline: 14,
            skills: ["Figma", "UI Design", "Analytics"],
            category: "UI/UX Design",
            location: "Remote",
            type: "individual",
            experience: "middle",
            responses: 12,
            status: "New",
            featured: true,
            client: {
                name: "TechSolutions Inc.",
                avatar: "neon-glow-blue",
                rating: 4.8,
                reviews: 15
            },
            stages: [
                { name: "Исследование и прототипы", duration: "4 дня" },
                { name: "Дизайн концепция", duration: "3 дня" },
                { name: "Финальные макеты", duration: "7 дней" }
            ]
        },
        {
            id: 2,
            title: "Landing Page для криптовалютного проекта",
            description: "Нужна разработка посадочной страницы с анимациями и интеграцией Web3 кошельков. Дизайн уже готов в Figma.",
            fullDescription: "Требуется Frontend разработчик для верстки лендинга по готовому макету. Важно реализовать сложные анимации на GSAP и подключение кошельков MetaMask/WalletConnect.",
            budget: 4200,
            deadline: 21,
            skills: ["React", "Web3", "GSAP"],
            category: "Web Development",
            location: "Remote",
            type: "individual",
            experience: "senior",
            responses: 8,
            status: "In Progress",
            featured: false,
            client: {
                name: "CryptoFuture",
                avatar: "neon-glow-pink",
                rating: 4.5,
                reviews: 8
            },
            stages: [
                { name: "Верстка", duration: "10 дней" },
                { name: "Анимации", duration: "5 дней" },
                { name: "Интеграция Web3", duration: "6 дней" }
            ]
        },
        {
            id: 3,
            title: "Дизайн мобильного приложения для фитнеса",
            description: "UI/UX дизайн iOS и Android приложения с трекингом тренировок и планами питания. Темная тема, неоновый стиль.",
            fullDescription: "Разработка дизайна мобильного приложения с нуля. Основной функционал: трекер тренировок, календарь питания, социальные функции.",
            budget: 5800,
            deadline: 30,
            skills: ["Mobile", "Fitness", "Health"],
            category: "Mobile App",
            location: "USA",
            type: "team",
            experience: "senior",
            responses: 5,
            status: "New",
            featured: false,
            urgent: true,
            client: {
                name: "FitLife Global",
                avatar: "neon-glow-purple",
                rating: 5.0,
                reviews: 22
            },
            stages: [
                { name: "UX Исследование", duration: "7 дней" },
                { name: "UI Дизайн", duration: "14 дней" },
                { name: "Адаптация под Android", duration: "9 дней" }
            ]
        },
        {
            id: 4,
            title: "Фирменный стиль для tech стартапа",
            description: "Разработка логотипа, брендбука, визиток и презентационных материалов для AI стартапа.",
            fullDescription: "Нам нужен смелый и футуристичный брендинг. Логотип, цветовая палитра, шрифты, паттерны.",
            budget: 2900,
            deadline: 18,
            skills: ["Logo", "Brand Identity", "AI"],
            category: "Branding",
            location: "Europe",
            type: "individual",
            experience: "middle",
            responses: 15,
            status: "New",
            featured: false,
            client: {
                name: "NeuroTech",
                avatar: "neon-glow-cyan",
                rating: 4.9,
                reviews: 10
            },
            stages: [
                { name: "Логотип", duration: "5 дней" },
                { name: "Айдентика", duration: "7 дней" },
                { name: "Брендбук", duration: "6 дней" }
            ]
        },
        {
            id: 5,
            title: "3D иллюстрации для веб-сайта",
            description: "Серия 3D иллюстраций в стиле cyberpunk для маркетингового сайта. 5 основных сцен.",
            fullDescription: "Создание 5 уникальных 3D сцен для лендинга. Стиль: Cyberpunk, Neon, Glassmorphism.",
            budget: 3200,
            deadline: 25,
            skills: ["3D", "Blender", "Cyberpunk"],
            category: "Illustration",
            location: "Remote",
            type: "individual",
            experience: "middle",
            responses: 9,
            status: "New",
            featured: true,
            client: {
                name: "CyberAgency",
                avatar: "neon-glow-orange",
                rating: 4.7,
                reviews: 18
            },
            stages: [
                { name: "Скетчинг", duration: "5 дней" },
                { name: "Моделирование", duration: "10 дней" },
                { name: "Рендеринг", duration: "10 дней" }
            ]
        },
        {
            id: 6,
            title: "E-commerce платформа на Next.js",
            description: "Полный цикл разработки интернет-магазина с админ-панелью и интеграцией платежей Stripe.",
            fullDescription: "Разработка масштабируемого интернет-магазина. Стек: Next.js, PostgreSQL, Prisma, Stripe, Tailwind.",
            budget: 7500,
            deadline: 45,
            skills: ["Next.js", "TypeScript", "Stripe"],
            category: "Full-Stack",
            location: "Remote",
            type: "team",
            experience: "senior",
            responses: 18,
            status: "Started",
            featured: false,
            client: {
                name: "ShopifyPlus",
                avatar: "neon-glow-green",
                rating: 4.6,
                reviews: 30
            },
            stages: [
                { name: "Бэкенд", duration: "15 дней" },
                { name: "Фронтенд", duration: "20 дней" },
                { name: "Тестирование", duration: "10 дней" }
            ]
        },
        // ... more mock data can be generated
    ];

    // Generate more mock data for infinite scroll
    for (let i = 7; i <= 20; i++) {
        mockProjects.push({
            id: i,
            title: `Project ${i} - ${['Design', 'Development', 'Marketing'][i % 3]}`,
            description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore.",
            fullDescription: "Detailed description of the project goes here. It includes all the necessary requirements and expectations.",
            budget: 1000 + (i * 100),
            deadline: 10 + i,
            skills: ["Skill A", "Skill B"],
            category: ['UI/UX Design', 'Web Development', 'Mobile App'][i % 3],
            location: "Remote",
            type: i % 2 === 0 ? "individual" : "team",
            experience: ['junior', 'middle', 'senior'][i % 3],
            responses: i * 2,
            status: "New",
            featured: false,
            client: {
                name: `Client ${i}`,
                avatar: "neon-glow-blue",
                rating: 4.0 + (i % 10) / 10,
                reviews: i
            },
            stages: []
        });
    }

    // --- State ---
    let currentProjects = [...mockProjects];
    let displayedProjects = [];
    let page = 1;
    const itemsPerPage = 6;
    let isLoading = false;

    // --- DOM Elements ---
    const projectsGrid = document.getElementById('projectsGrid');
    const loadingIndicator = document.getElementById('loadingIndicator');
    const emptyState = document.getElementById('emptyState');
    const projectsCount = document.getElementById('projectsCount');
    const sortSelect = document.getElementById('sortSelect');

    // Filters
    const mainSearchInput = document.getElementById('mainSearchInput');
    const mainSearchBtn = document.getElementById('mainSearchBtn');
    const minBudgetInput = document.getElementById('minBudget');
    const maxBudgetInput = document.getElementById('maxBudget');
    const maxDeadlineInput = document.getElementById('maxDeadline');
    const locationInput = document.getElementById('locationInput');
    const projectTypeInputs = document.querySelectorAll('input[name="projectType"]');
    const experienceLevelInputs = document.querySelectorAll('input[name="experienceLevel"]');
    const applyFiltersBtn = document.getElementById('applyFiltersBtn');
    const skillInput = document.getElementById('skillInput');
    const selectedSkillsContainer = document.getElementById('selectedSkills');

    // Modal
    const modal = document.getElementById('projectDetailsModal');
    const modalBody = document.getElementById('projectModalBody');
    const closeModal = document.getElementById('closeProjectModal');

    // --- Functions ---

    function renderProjectCard(project) {
        const categoryColors = {
            'UI/UX Design': 'neon-glow-blue',
            'Web Development': 'neon-glow-pink',
            'Mobile App': 'neon-glow-purple',
            'Branding': 'neon-glow-cyan',
            'Illustration': 'neon-glow-orange',
            'Full-Stack': 'neon-glow-green'
        };
        const colorClass = categoryColors[project.category] || 'neon-glow-blue';
        const priceColor = project.category === 'Branding' ? 'style="color: var(--neon-cyan);"' : `class="price-value ${colorClass.replace('glow', 'text')}"`;

        return `
            <div class="marketplace-card glass-card" data-id="${project.id}">
                <div class="card-header">
                    <div class="card-category ${colorClass}">${project.category}</div>
                    ${project.featured ? '<div class="card-badge featured">Featured</div>' : ''}
                    ${project.urgent ? '<div class="card-badge urgent">Срочно</div>' : ''}
                </div>
                <h3 class="card-title">${project.title}</h3>
                <p class="card-description">${project.description}</p>
                <div class="card-tags">
                    ${project.skills.map(skill => `<span class="tag">${skill}</span>`).join('')}
                </div>
                <div class="card-footer">
                    <div class="card-price">
                        <span class="price-label">Бюджет:</span>
                        <span ${priceColor}>$${project.budget}</span>
                    </div>
                    <div class="card-meta">
                        <div class="meta-item">
                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                                <path d="M8 2V8L11 11" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" />
                                <circle cx="8" cy="8" r="6" stroke="currentColor" stroke-width="1.5" />
                            </svg>
                            <span>${project.deadline} дней</span>
                        </div>
                        <div class="meta-item">
                            <svg width="16" height="16" viewBox="0 0 16 16" fill="none">
                                <path d="M12 7L8 3L4 7M4 9L8 13L12 9" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" />
                            </svg>
                            <span>${project.responses} откликов</span>
                        </div>
                    </div>
                </div>
                <button class="apply-btn neon-button" onclick="openProjectModal(${project.id})">Откликнуться</button>
            </div>
        `;
    }

    function loadProjects(reset = false) {
        if (isLoading) return;
        isLoading = true;

        if (reset) {
            projectsGrid.innerHTML = '';
            page = 1;
            displayedProjects = [];
            loadingIndicator.style.display = 'flex';
            emptyState.style.display = 'none';
        }

        // Simulate API delay
        setTimeout(() => {
            const start = (page - 1) * itemsPerPage;
            const end = start + itemsPerPage;
            const newProjects = currentProjects.slice(start, end);

            if (newProjects.length === 0 && page === 1) {
                emptyState.style.display = 'flex';
                loadingIndicator.style.display = 'none';
                isLoading = false;
                return;
            }

            newProjects.forEach(project => {
                projectsGrid.insertAdjacentHTML('beforeend', renderProjectCard(project));
            });

            displayedProjects = [...displayedProjects, ...newProjects];
            projectsCount.textContent = currentProjects.length;

            if (newProjects.length < itemsPerPage) {
                // No more projects to load
                loadingIndicator.style.display = 'none';
            } else {
                loadingIndicator.style.display = 'flex'; // Keep showing if more might exist
            }

            page++;
            isLoading = false;

            // Re-attach event listeners for new buttons if needed (using onclick in HTML for simplicity here)
        }, 800);
    }

    function filterProjects() {
        const searchQuery = mainSearchInput.value.toLowerCase();
        const minBudget = parseInt(minBudgetInput.value) || 0;
        const maxBudget = parseInt(maxBudgetInput.value) || 1000000;
        const maxDeadline = parseInt(maxDeadlineInput.value) || 365;
        const location = locationInput.value.toLowerCase();

        const selectedTypes = Array.from(projectTypeInputs).filter(cb => cb.checked).map(cb => cb.value);
        const selectedExperience = Array.from(experienceLevelInputs).filter(cb => cb.checked).map(cb => cb.value);

        // Skills logic (simplified for text input)
        const skillQuery = skillInput.value.toLowerCase();

        currentProjects = mockProjects.filter(project => {
            const matchesSearch = project.title.toLowerCase().includes(searchQuery) || project.description.toLowerCase().includes(searchQuery);
            const matchesBudget = project.budget >= minBudget && project.budget <= maxBudget;
            const matchesDeadline = project.deadline <= maxDeadline;
            const matchesLocation = location === '' || project.location.toLowerCase().includes(location);
            const matchesType = selectedTypes.includes(project.type);
            const matchesExperience = selectedExperience.includes(project.experience);
            const matchesSkill = skillQuery === '' || project.skills.some(s => s.toLowerCase().includes(skillQuery));

            return matchesSearch && matchesBudget && matchesDeadline && matchesLocation && matchesType && matchesExperience && matchesSkill;
        });

        sortProjects(false); // Sort but don't reload yet
        loadProjects(true); // Reset and reload
    }

    function sortProjects(reload = true) {
        const sortValue = sortSelect.value;

        currentProjects.sort((a, b) => {
            if (sortValue === 'newest') return b.id - a.id; // Mock ID as date
            if (sortValue === 'budget_asc') return a.budget - b.budget;
            if (sortValue === 'budget_desc') return b.budget - a.budget;
            if (sortValue === 'deadline') return a.deadline - b.deadline;
            return 0;
        });

        if (reload) loadProjects(true);
    }

    // --- Modal Logic ---
    window.openProjectModal = function (id) {
        const project = mockProjects.find(p => p.id === id);
        if (!project) return;

        const stagesHtml = project.stages ? project.stages.map(s => `
            <div class="stage-item">
                <span class="stage-name">${s.name}</span>
                <span class="stage-duration">${s.duration}</span>
            </div>
        `).join('') : '';

        modalBody.innerHTML = `
            <div class="modal-header-content">
                <h2 class="modal-title">${project.title}</h2>
                <div class="modal-badges">
                    <span class="modal-badge status-${project.status.toLowerCase().replace(' ', '-')}">${project.status}</span>
                    <span class="modal-badge category">${project.category}</span>
                </div>
            </div>
            
            <div class="modal-grid">
                <div class="modal-main">
                    <div class="modal-section">
                        <h3>Описание проекта</h3>
                        <p>${project.fullDescription.replace(/\n/g, '<br>')}</p>
                    </div>
                    
                    ${stagesHtml ? `
                    <div class="modal-section">
                        <h3>Этапы работы</h3>
                        <div class="stages-list">
                            ${stagesHtml}
                        </div>
                    </div>
                    ` : ''}
                </div>
                
                <div class="modal-sidebar">
                    <div class="client-card glass-card">
                        <div class="client-header">
                            <div class="client-avatar ${project.client.avatar}"></div>
                            <div class="client-info">
                                <div class="client-name">${project.client.name}</div>
                                <div class="client-rating">⭐ ${project.client.rating} (${project.client.reviews} отзывов)</div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="project-stats glass-card">
                        <div class="stat-row">
                            <span>Бюджет:</span>
                            <span class="stat-value">$${project.budget}</span>
                        </div>
                        <div class="stat-row">
                            <span>Срок:</span>
                            <span class="stat-value">${project.deadline} дней</span>
                        </div>
                        <div class="stat-row">
                            <span>Опыт:</span>
                            <span class="stat-value" style="text-transform: capitalize;">${project.experience}</span>
                        </div>
                    </div>
                    
                    <button class="apply-btn neon-button full-width" onclick="alert('Заявка отправлена!')">Подать заявку</button>
                </div>
            </div>
        `;

        modal.classList.add('active');
        document.body.style.overflow = 'hidden';
    };

    closeModal.addEventListener('click', () => {
        modal.classList.remove('active');
        document.body.style.overflow = '';
    });

    modal.addEventListener('click', (e) => {
        if (e.target === modal) {
            modal.classList.remove('active');
            document.body.style.overflow = '';
        }
    });

    // --- Infinite Scroll ---
    window.addEventListener('scroll', () => {
        if (window.innerHeight + window.scrollY >= document.body.offsetHeight - 500) {
            loadProjects();
        }
    });

    // --- Event Listeners ---
    applyFiltersBtn.addEventListener('click', filterProjects);
    mainSearchBtn.addEventListener('click', filterProjects);
    mainSearchInput.addEventListener('keypress', (e) => {
        if (e.key === 'Enter') filterProjects();
    });
    sortSelect.addEventListener('change', () => sortProjects(true));

    // AI Recommendations (Mock)
    const aiContainer = document.getElementById('aiRecommendationsContainer');
    setTimeout(() => {
        aiContainer.innerHTML = `
            <div class="ai-project-mini">
                <div class="ai-match">98% Match</div>
                <h4 class="ai-project-title">AI Dashboard UI</h4>
                <p class="ai-project-brief">Сложный интерфейс для AI аналитики...</p>
                <div class="ai-project-info">
                    <span class="ai-budget">$4,500</span>
                    <span class="ai-deadline">3 нед</span>
                </div>
                <button class="ai-apply-btn" onclick="openProjectModal(1)">Посмотреть</button>
            </div>
            <div class="ai-project-mini">
                <div class="ai-match">92% Match</div>
                <h4 class="ai-project-title">Crypto Wallet App</h4>
                <p class="ai-project-brief">Мобильное приложение для крипты...</p>
                <div class="ai-project-info">
                    <span class="ai-budget">$6,000</span>
                    <span class="ai-deadline">4 нед</span>
                </div>
                <button class="ai-apply-btn" onclick="openProjectModal(3)">Посмотреть</button>
            </div>
        `;
    }, 1500);

    // Initial Load
    loadProjects(true);
});


// Success Modal Logic
document.addEventListener('DOMContentLoaded', () => {
    const successModal = document.getElementById('successModal');
    const closeSuccessModal = document.getElementById('closeSuccessModal');
    const successContinueBtn = document.getElementById('successContinueBtn');
    // Select both regular apply buttons and AI apply buttons
    const applyBtns = document.querySelectorAll('.apply-btn, .ai-apply-btn');

    if (successModal && applyBtns.length > 0) {
        const openSuccessModal = () => {
            successModal.classList.add('active');
            document.body.style.overflow = 'hidden';
        };

        const closeSuccess = () => {
            successModal.classList.remove('active');
            document.body.style.overflow = '';
        };

        applyBtns.forEach(btn => {
            btn.addEventListener('click', (e) => {
                e.preventDefault(); // Prevent default if it's a link
                openSuccessModal();
            });
        });

        if (closeSuccessModal) closeSuccessModal.addEventListener('click', closeSuccess);
        if (successContinueBtn) successContinueBtn.addEventListener('click', closeSuccess);

        successModal.addEventListener('click', (e) => {
            if (e.target === successModal) {
                closeSuccess();
            }
        });
    }

    // Education Creator Dashboard Logic
    const creatorTabs = document.querySelectorAll('.creator-tab');
    if (creatorTabs.length > 0) {
        creatorTabs.forEach(tab => {
            tab.addEventListener('click', () => {
                // Remove active class from all tabs
                creatorTabs.forEach(t => t.classList.remove('active'));
                tab.classList.add('active');

                // Hide all sections
                document.querySelectorAll('.creator-section').forEach(s => s.classList.remove('active'));

                // Show target section
                const targetId = `section-${tab.dataset.tab}`;
                const targetSection = document.getElementById(targetId);
                if (targetSection) {
                    targetSection.classList.add('active');
                }
            });
        });
    }

    // Wizard Logic
    window.selectContentType = function (type) {
        const wizard = document.getElementById('creationWizard');
        const typeGrid = document.querySelector('.content-type-grid');

        if (wizard && typeGrid) {
            typeGrid.style.display = 'none';
            wizard.style.display = 'block';
            // Reset wizard to step 1
            document.querySelectorAll('.step').forEach(s => s.classList.remove('active'));
            document.querySelector('.step[data-step="1"]').classList.add('active');
            document.querySelectorAll('.wizard-step-content').forEach(c => c.classList.remove('active'));
            document.getElementById('step1').classList.add('active');
        }
    };

    window.closeWizard = function () {
        const wizard = document.getElementById('creationWizard');
        const typeGrid = document.querySelector('.content-type-grid');

        if (wizard && typeGrid) {
            wizard.style.display = 'none';
            typeGrid.style.display = 'grid';
        }
    };

    let currentStep = 1;
    window.nextStep = function () {
        if (currentStep < 5) {
            currentStep++;
            updateWizardStep();
        } else {
            // Finish wizard
            // Show success message or toast
            alert('Курс успешно создан и отправлен на модерацию!');
            closeWizard();
            // Reset wizard
            currentStep = 1;
            updateWizardStep();
        }
    };

    window.prevStep = function () {
        if (currentStep > 1) {
            currentStep--;
            updateWizardStep();
        }
    };

    function updateWizardStep() {
        // Update steps indicator
        document.querySelectorAll('.step').forEach(s => {
            const stepNum = parseInt(s.dataset.step);
            if (stepNum === currentStep) {
                s.classList.add('active');
            } else if (stepNum < currentStep) {
                s.classList.remove('active'); // Or keep active to show progress
            } else {
                s.classList.remove('active');
            }
        });

        // Show content
        document.querySelectorAll('.wizard-step-content').forEach(c => c.classList.remove('active'));
        const targetContent = document.getElementById(`step${currentStep}`);
        if (targetContent) {
            targetContent.classList.add('active');
        }

        // Update Button Text
        const nextBtn = document.querySelector('.wizard-footer .btn-primary');
        if (nextBtn) {
            if (currentStep === 5) {
                nextBtn.textContent = 'Готово';
                nextBtn.classList.add('btn-success'); // Optional style change
            } else {
                nextBtn.textContent = 'Далее';
                nextBtn.classList.remove('btn-success');
            }
        }
    }
});

/* ========================================
   MARKETPLACE FILTERS LOGIC
   ======================================== */
document.addEventListener('DOMContentLoaded', () => {
    const applyFiltersBtn = document.querySelector('.btn-primary.full-width');
    if (applyFiltersBtn) {
        applyFiltersBtn.addEventListener('click', () => {
            // Collect filter values
            const location = document.querySelector('.location-input')?.value;
            const projectType = document.querySelector('input[name="projectType"]:checked')?.value;
            const experience = Array.from(document.querySelectorAll('input[type="checkbox"]:checked'))
                .map(cb => cb.value);

            console.log('Filters Applied:', {
                location,
                projectType,
                experience
            });

            // Visual feedback
            const originalText = applyFiltersBtn.innerText;
            applyFiltersBtn.innerText = 'Фильтры применены!';
            applyFiltersBtn.style.background = 'var(--neon-green)';

            setTimeout(() => {
                applyFiltersBtn.innerText = originalText;
                applyFiltersBtn.style.background = '';
            }, 2000);
        });
    }
});

/* ========================================
   INFINITE SCROLL & EMPTY STATE LOGIC
   ======================================== */
document.addEventListener('DOMContentLoaded', () => {
    const projectsGrid = document.querySelector('.projects-grid-container');
    const scrollLoader = document.querySelector('.scroll-loader');
    const emptyState = document.querySelector('.empty-state');

    // Mock Data for Infinite Scroll
    const mockProjects = [
        {
            title: "Разработка CRM системы",
            category: "Web Development",
            budget: "$5,000",
            deadline: "45 дней",
            tags: ["React", "Node.js", "PostgreSQL"],
            responses: 12,
            color: "pink"
        },
        {
            title: "Логотип для кофейни",
            category: "Branding",
            budget: "$800",
            deadline: "7 дней",
            tags: ["Illustrator", "Logo", "Brand"],
            responses: 24,
            color: "cyan"
        },
        {
            title: "iOS приложение доставки",
            category: "Mobile App",
            budget: "$6,500",
            deadline: "60 дней",
            tags: ["Swift", "iOS", "MapKit"],
            responses: 8,
            color: "purple"
        }
    ];

    // Infinite Scroll Observer
    if (scrollLoader && projectsGrid) {
        const observerOptions = {
            root: null,
            rootMargin: '100px',
            threshold: 0.1
        };

        const observer = new IntersectionObserver((entries) => {
            entries.forEach(entry => {
                if (entry.isIntersecting) {
                    loadMoreProjects();
                }
            });
        }, observerOptions);

        observer.observe(scrollLoader);

        function loadMoreProjects() {
            scrollLoader.classList.add('active');

            // Simulate API delay
            setTimeout(() => {
                mockProjects.forEach(project => {
                    const card = createProjectCard(project);
                    projectsGrid.appendChild(card);
                });
                scrollLoader.classList.remove('active');
            }, 1500);
        }
    }

    // Helper to create card HTML
    function createProjectCard(data) {
        const div = document.createElement('div');
        div.className = 'marketplace-card glass-card';
        div.innerHTML = `
            <div class="card-header">
                <div class="card-category neon-glow-${data.color}">${data.category}</div>
            </div>
            <h3 class="card-title">${data.title}</h3>
            <p class="card-description">Автоматически загруженный проект для демонстрации бесконечного скролла.</p>
            <div class="card-tags">
                ${data.tags.map(tag => `<span class="tag">${tag}</span>`).join('')}
            </div>
            <div class="card-footer">
                <div class="card-price">
                    <span class="price-label">Бюджет:</span>
                    <span class="price-value neon-text-${data.color}">${data.budget}</span>
                </div>
                <div class="card-meta">
                    <div class="meta-item">
                        <svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M8 2V8L11 11" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/><circle cx="8" cy="8" r="6" stroke="currentColor" stroke-width="1.5"/></svg>
                        <span>${data.deadline}</span>
                    </div>
                    <div class="meta-item">
                        <svg width="16" height="16" viewBox="0 0 16 16" fill="none"><path d="M12 7L8 3L4 7M4 9L8 13L12 9" stroke="currentColor" stroke-width="1.5" stroke-linecap="round"/></svg>
                        <span>${data.responses} откликов</span>
                    </div>
                </div>
            </div>
            <button class="apply-btn neon-button">Подробнее</button>
        `;
        return div;
    }
});

/* ========================================
   MY WORK PAGE LOGIC
   ======================================== */
document.addEventListener('DOMContentLoaded', () => {
    if (!document.querySelector('.my-work-page')) return;

    // Tabs Logic
    const tabs = document.querySelectorAll('.work-tab');
    const contents = document.querySelectorAll('.tab-content');

    tabs.forEach(tab => {
        tab.addEventListener('click', () => {
            // Remove active class from all
            tabs.forEach(t => t.classList.remove('active'));
            contents.forEach(c => c.classList.remove('active'));

            // Add active class to clicked
            tab.classList.add('active');
            const targetId = tab.dataset.tab;
            document.getElementById(targetId).classList.add('active');
        });
    });

    // Mock Data Rendering
    renderIndividualProjects();
    renderTeamProjects();
    renderApplications();
    renderInvitations();

    function renderIndividualProjects() {
        const container = document.getElementById('individualProjectsList');
        const projects = [
            { title: 'Редизайн Dashboard', client: 'Alex M.', progress: 75, status: 'active', deadline: '3 дня', type: 'UI/UX' },
            { title: 'Логотип для стартапа', client: 'Sarah C.', progress: 30, status: 'active', deadline: '10 дней', type: 'Branding' },
            { title: 'Верстка лендинга', client: 'Mike R.', progress: 100, status: 'review', deadline: 'На проверке', type: 'Frontend' },
            { title: 'Скрипт парсинга', client: 'DevCorp', progress: 100, status: 'completed', deadline: 'Завершен', type: 'Backend' }
        ];

        container.innerHTML = projects.map(p => `
            <div class="project-list-card">
                <div class="project-main-info">
                    <h3>${p.title}</h3>
                    <span class="client-name-small">Заказчик: ${p.client}</span>
                </div>
                <div class="project-progress">
                    <div class="progress-text">
                        <span>Прогресс</span>
                        <span>${p.progress}%</span>
                    </div>
                    <div class="progress-bar-bg">
                        <div class="progress-bar-fill" style="width: ${p.progress}%"></div>
                    </div>
                </div>
                <div class="project-meta-col">
                    <span><i class="far fa-clock"></i> ${p.deadline}</span>
                    <span>${p.type}</span>
                </div>
                <div class="status-badge status-${p.status}">
                    ${getStatusLabel(p.status)}
                </div>
                <div class="project-actions">
                    <button class="project-action-btn" title="Чат"><i class="far fa-comment-alt"></i></button>
                    <button class="project-action-btn" title="Файлы"><i class="far fa-folder"></i></button>
                    <button class="project-action-btn" title="Подробнее"><i class="fas fa-ellipsis-h"></i></button>
                </div>
            </div>
        `).join('');
    }

    function renderTeamProjects() {
        const container = document.getElementById('teamProjectsList');
        const projects = [
            {
                title: 'SaaS Платформа "EcoTrack"',
                role: 'Lead Designer',
                members: 4,
                tasks: '12/45',
                progress: 27,
                nextCall: '14:00',
                icon: 'fas fa-leaf',
                color: 'var(--neon-green)'
            },
            {
                title: 'Мобильное приложение "FitLife"',
                role: 'UI Designer',
                members: 6,
                tasks: '8/30',
                progress: 26,
                nextCall: 'Завтра',
                icon: 'fas fa-heartbeat',
                color: 'var(--neon-pink)'
            }
        ];

        container.innerHTML = projects.map(p => `
            <div class="team-project-card">
                <div class="tp-header">
                    <div class="tp-icon-wrapper" style="color: ${p.color}; background: ${p.color}20;">
                        <i class="${p.icon}"></i>
                    </div>
                    <div class="tp-info">
                        <h3>${p.title}</h3>
                        <span class="tp-role">${p.role}</span>
                    </div>
                    <div class="tp-menu">
                        <button class="icon-btn-small"><i class="fas fa-ellipsis-h"></i></button>
                    </div>
                </div>
                
                <div class="tp-progress-section">
                    <div class="tp-progress-labels">
                        <span>Прогресс</span>
                        <span>${p.progress}%</span>
                    </div>
                    <div class="tp-progress-bar">
                        <div class="tp-progress-fill" style="width: ${p.progress}%; background: ${p.color}; box-shadow: 0 0 10px ${p.color};"></div>
                    </div>
                </div>

                <div class="tp-footer">
                    <div class="tp-team">
                        <div class="team-avatars-stack">
                            ${Array(p.members).fill(0).map((_, i) => `<img src="https://ui-avatars.com/api/?name=User+${i}&background=random" alt="User">`).join('')}
                        </div>
                        <button class="add-member-btn"><i class="fas fa-plus"></i></button>
                    </div>
                    <div class="tp-actions">
                        <button class="tp-action-btn" title="Звонок"><i class="fas fa-phone-alt"></i></button>
                        <button class="tp-action-btn" title="Доска"><i class="fas fa-columns"></i></button>
                        <button class="tp-action-btn primary" title="Открыть">Открыть</button>
                    </div>
                </div>
            </div>
        `).join('');
    }

    function renderApplications() {
        const container = document.getElementById('applicationsList');
        const apps = [
            { project: 'Корпоративный сайт', date: '28.11.2024', status: 'viewed', bid: '$1,200' },
            { project: 'Дизайн презентации', date: '27.11.2024', status: 'sent', bid: '$300' },
            { project: 'React Native App', date: '25.11.2024', status: 'rejected', bid: '$4,000' },
            { project: '3D Моделирование', date: '20.11.2024', status: 'accepted', bid: '$800' }
        ];

        container.innerHTML = apps.map(a => `
            <tr>
                <td>${a.project}</td>
                <td>${a.date}</td>
                <td><span class="app-status ${a.status}"><i class="fas fa-circle" style="font-size: 8px;"></i> ${getAppStatusLabel(a.status)}</span></td>
                <td class="neon-text-blue">${a.bid}</td>
                <td>
                    <button class="app-delete-btn" title="Удалить"><i class="far fa-trash-alt"></i></button>
                </td>
            </tr>
        `).join('');
    }

    function renderInvitations() {
        const container = document.getElementById('invitationsList');
        const invites = [
            { project: 'Crypto Exchange UI', role: 'Senior Designer', rate: '$45/hr', inviter: 'Blockchain Inc.' }
        ];

        container.innerHTML = invites.map(i => `
            <div class="invitation-card">
                <div class="invitation-icon">
                    <i class="fas fa-envelope-open-text"></i>
                </div>
                <div class="invitation-content">
                    <div class="invitation-role">${i.role}</div>
                    <div class="invitation-project">${i.project}</div>
                    <div class="invitation-details">
                        <span><i class="far fa-building"></i> ${i.inviter}</span>
                        <span><i class="fas fa-dollar-sign"></i> ${i.rate}</span>
                    </div>
                </div>
                <div class="invitation-actions">
                    <button class="btn-primary neon-button">Принять</button>
                    <button class="btn-secondary">Отклонить</button>
                </div>
            </div>
        `).join('');
    }

    function getStatusLabel(status) {
        const map = { 'active': 'В работе', 'review': 'На проверке', 'completed': 'Завершен' };
        return map[status] || status;
    }

    function getAppStatusLabel(status) {
        const map = { 'sent': 'Отправлен', 'viewed': 'Просмотрен', 'accepted': 'Принят', 'rejected': 'Отклонен' };
        return map[status] || status;
    }
});
